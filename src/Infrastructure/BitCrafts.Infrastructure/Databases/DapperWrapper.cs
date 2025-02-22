using System.Data;
using Dapper;
namespace BitCrafts.Infrastructure.Abstraction.Databases;

public class DapperWrapper : IDapperWrapper
{
    public Task<T> QuerySingleAsync<T>(IDbConnection connection, string sql, object param = null, IDbTransaction transaction = null)
        => connection.QuerySingleAsync<T>(sql, param, transaction);

    public Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, object param = null, IDbTransaction transaction = null)
        => connection.QueryAsync<T>(sql, param, transaction);

    public Task<int> ExecuteAsync(IDbConnection connection, string sql, object param = null, IDbTransaction transaction = null)
        => connection.ExecuteAsync(sql, param, transaction);
}
