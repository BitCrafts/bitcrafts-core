using System.Data.Common;
using BitCrafts.Infrastructure.Abstraction.Databases;

namespace BitCrafts.Infrastructure.Databases;

public sealed class DatabaseTransaction : IDatabaseTransaction
{
    private readonly DbTransaction _transaction;
    private readonly DbConnection _connection;

    public DbTransaction DbTransaction => _transaction as DbTransaction;

    public DatabaseTransaction(DbTransaction transaction, DbConnection connection)
    {
        _transaction = transaction;
        _connection = connection;
    }


    public async Task CommitAsync()
    {
        await _transaction.CommitAsync().ConfigureAwait(false);
    }

    public async Task RollbackAsync()
    {
        await _transaction.RollbackAsync().ConfigureAwait(false);
    }

    public void Dispose()
    {
        _transaction.Dispose();
        _connection.Dispose();
    }
}