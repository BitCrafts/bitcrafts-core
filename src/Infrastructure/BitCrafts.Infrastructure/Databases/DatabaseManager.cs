using System.Data;
using BitCrafts.Infrastructure.Abstraction.Databases;

namespace BitCrafts.Infrastructure.Databases;

public class DatabaseManager : IDatabaseManager
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly IDapperWrapper _dapper;
    private readonly ISqlDialectFactory _dialectFactory;

    public DatabaseManager(IDbConnectionFactory connectionFactory, IDapperWrapper dapper,
        ISqlDialectFactory dialectFactory)
    {
        _connectionFactory = connectionFactory;
        _dapper = dapper;
        _dialectFactory = dialectFactory;
    }

    private async Task<TResult> UseConnectionAsync<TResult>(Func<IDbConnection, ISqlDialect, Task<TResult>> operation)
    {
        using var connection = _connectionFactory.Create();
        connection.Open();

        var dialect = _dialectFactory.Create();
        return await operation(connection, dialect);
    }

    public async Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null)
    {
        return await UseConnectionAsync((conn, _) => _dapper.QuerySingleAsync<T>(conn, sql, param, transaction));
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null)
    {
        return await UseConnectionAsync((conn, _) => _dapper.QueryAsync<T>(conn, sql, param, transaction));
    }

    public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null)
    {
        return await UseConnectionAsync((conn, _) => _dapper.ExecuteAsync(conn, sql, param, transaction));
    }

    public async Task<int> GetLastInsertedIdAsync()
    {
        return await UseConnectionAsync(async (conn, dialect) =>
        {
            var query = dialect.GetLastInsertedIdQuery();
            if (string.IsNullOrEmpty(query))
            {
                throw new NotSupportedException("This database does not support last inserted ID queries directly.");
            }

            return await _dapper.QuerySingleAsync<int>(conn, query);
        });
    }
}