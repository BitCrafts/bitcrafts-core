using BitCrafts.Infrastructure.Abstraction.Databases;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Repositories;

namespace BitCrafts.Users.Repositories;

public sealed class UsersRepository : IUsersRepository
{
    private readonly IDatabaseManager _dbManager;

    public UsersRepository(IDatabaseManager dbManager)
    {
        _dbManager = dbManager;
    }

    public async Task<IUser> AddAsync(IUser entity, IDatabaseTransaction transaction = null)
    {
        await _dbManager.ExecuteAsync(
            "INSERT INTO User (FirstName, LastName, Email, PhoneNumber, BirthDate, NationalNumber, PassportNumber) " +
            "VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @BirthDate, @NationalNumber, @PassportNumber);",
            entity, transaction?.DbTransaction);

        var id = await _dbManager.GetLastInsertedIdAsync();
        entity.Id = id;
        return entity;
    }

    public async Task<IEnumerable<IUser>> GetAllAsync(IDatabaseTransaction transaction = null)
    {
        return await _dbManager.QueryAsync<IUser>(
            "SELECT * FROM User;", transaction?.DbTransaction);
    }

    public async Task<IUser> GetByIdAsync(int id, IDatabaseTransaction transaction = null)
    {
        return await _dbManager.QuerySingleAsync<IUser>(
            "SELECT * FROM User WHERE Id = @Id;",
            new { Id = id }, transaction?.DbTransaction);
    }

    public async Task<bool> UpdateAsync(IUser entity, IDatabaseTransaction transaction = null)
    {
        var result = await _dbManager.ExecuteAsync(
            "UPDATE User SET FirstName = @FirstName, LastName = @LastName, Email = @Email, " +
            "PhoneNumber = @PhoneNumber, BirthDate = @BirthDate, NationalNumber = @NationalNumber, PassportNumber = @PassportNumber " +
            "WHERE Id = @Id;",
            entity, transaction?.DbTransaction);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id, IDatabaseTransaction transaction = null)
    {
        var result = await _dbManager.ExecuteAsync(
            "DELETE FROM User WHERE Id = @Id;",
            new { Id = id }, transaction?.DbTransaction);

        return result > 0;
    }
}