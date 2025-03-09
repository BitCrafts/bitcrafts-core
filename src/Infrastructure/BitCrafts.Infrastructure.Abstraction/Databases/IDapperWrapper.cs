using System.Data;

namespace BitCrafts.Infrastructure.Abstraction.Databases;

public interface IDapperWrapper
{
    Task<T> QuerySingleAsync<T>(IDbConnection connection, string sql, object param = null,
        IDbTransaction transaction = null);

    Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, object param = null,
        IDbTransaction transaction = null);

    Task<int> ExecuteAsync(IDbConnection connection, string sql, object param = null,
        IDbTransaction transaction = null);
}