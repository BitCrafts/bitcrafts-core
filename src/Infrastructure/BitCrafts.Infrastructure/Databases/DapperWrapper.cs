using System.Data;
using BitCrafts.Infrastructure.Abstraction.Databases;
using Dapper;

namespace BitCrafts.Infrastructure.Databases;

public sealed class DapperWrapper : IDapperWrapper
{
    public Task<T> QuerySingleAsync<T>(IDbConnection connection, string sql, object param = null,
        IDbTransaction transaction = null)
    {
        return connection.QuerySingleAsync<T>(sql, param, transaction);
    }

    public Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, object param = null,
        IDbTransaction transaction = null)
    {
        return connection.QueryAsync<T>(sql, param, transaction);
    }

    public Task<int> ExecuteAsync(IDbConnection connection, string sql, object param = null,
        IDbTransaction transaction = null)
    {
        return connection.ExecuteAsync(sql, param, transaction);
    }
}