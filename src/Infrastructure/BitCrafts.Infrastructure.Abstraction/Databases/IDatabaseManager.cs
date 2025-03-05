using System.Data;

namespace BitCrafts.Infrastructure.Abstraction.Databases;

public interface IDatabaseManager
{
    Task<IDbConnection> OpenNewConnection();
    Task<int> GetLastInsertedIdAsync();

    Task<IDbTransaction> BeginTransactionAsync();

}