using BitCrafts.Infrastructure.Abstraction.Databases;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Repositories;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Users.Repositories;

public sealed class UserAccountsRepository : IUserAccountsRepository
{
    private readonly IDatabaseManager _dbManager;
    private readonly ILogger<UserAccountsRepository> _logger;

    public UserAccountsRepository(IDatabaseManager dbManager, ILogger<UserAccountsRepository> logger)
    {
        _dbManager = dbManager;
        _logger = logger;
    }

    public async Task<IUserAccount> AddAsync(IUserAccount entity, IDatabaseTransaction transaction = null)
    {
        try
        {
            await _dbManager.ExecuteAsync(
                "INSERT INTO UserAccount (UserId, HashedPassword, PasswordSalt) " +
                "VALUES (@UserId, @HashedPassword, @PasswordSalt);", entity, transaction?.DbTransaction);

            var id = await _dbManager.GetLastInsertedIdAsync();
            entity.Id = id;
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding user account.");
            throw;
        }
    }

    public async Task<IEnumerable<IUserAccount>> GetAllAsync(IDatabaseTransaction transaction = null)
    {
        return await _dbManager.QueryAsync<IUserAccount>("SELECT * FROM UserAccount;", transaction?.DbTransaction);
    }

    public async Task<IUserAccount> GetByIdAsync(int id, IDatabaseTransaction transaction = null)
    {
        return await _dbManager.QuerySingleAsync<IUserAccount>("SELECT * FROM UserAccount WHERE Id = @Id;",
            new { Id = id }, transaction?.DbTransaction);
    }

    public async Task<IUserAccount> GetByUserIdAsync(int userId, IDatabaseTransaction transaction = null)
    {
        return await _dbManager.QuerySingleAsync<IUserAccount>("SELECT * FROM UserAccount WHERE UserId = @UserId;",
            new { UserId = userId }, transaction?.DbTransaction);
    }

    public async Task<bool> UpdateAsync(IUserAccount entity, IDatabaseTransaction transaction = null)
    {
        int rowsAffected = await _dbManager.ExecuteAsync(
            "UPDATE UserAccount SET HashedPassword = @HashedPassword, PasswordSalt = @PasswordSalt " +
            "WHERE Id = @Id;", entity, transaction?.DbTransaction
        );

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id, IDatabaseTransaction transaction = null)
    {
        var result = await _dbManager.ExecuteAsync("DELETE FROM UserAccount WHERE Id = @Id;", new { Id = id },
            transaction?.DbTransaction);

        return result > 0;
    }
}