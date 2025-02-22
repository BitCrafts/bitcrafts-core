using System.Data;
using BitCrafts.Infrastructure.Abstraction.Databases;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Repositories;
using BitCrafts.Users.Entities;
using Dapper;

namespace BitCrafts.Users.Repositories;

public sealed class UsersRepository : IUsersRepository
{
    private readonly IDatabaseManager _dbManager;

    public UsersRepository(IDatabaseManager dbManager)
    {
        _dbManager = dbManager;
    }

    public async Task<IUser> AddAsync(IUser entity)
    {
        await _dbManager.ExecuteAsync(
            "INSERT INTO Users (FirstName, LastName, Email, Password, PhoneNumber, BirthDate, NationalNumber, PassportNumber) " +
            "VALUES (@FirstName, @LastName, @Email, @Password, @PhoneNumber, @BirthDate, @NationalNumber, @PassportNumber);",
            entity);

        var id = await _dbManager.GetLastInsertedIdAsync();
        entity.Id = id;
        return entity;
    }

    public async Task<IEnumerable<IUser>> GetAllAsync()
    {
        return await _dbManager.QueryAsync<IUser>(
            "SELECT * FROM Users;");
    }

    public async Task<IUser> GetByIdAsync(int id)
    {
        return await _dbManager.QuerySingleAsync<IUser>(
            "SELECT * FROM Users WHERE Id = @Id;",
            new { Id = id });
    }

    public async Task<bool> UpdateAsync(IUser entity)
    {
        var result = await _dbManager.ExecuteAsync(
            "UPDATE Users SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Password = @Password, " +
            "PhoneNumber = @PhoneNumber, BirthDate = @BirthDate, NationalNumber = @NationalNumber, PassportNumber = @PassportNumber " +
            "WHERE Id = @Id;",
            entity);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _dbManager.ExecuteAsync(
            "DELETE FROM Users WHERE Id = @Id;",
            new { Id = id });

        return result > 0;
    }
}