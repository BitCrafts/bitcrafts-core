using System.Data;
using System.Data.Common;
using BitCrafts.Infrastructure.Abstraction.Databases;

namespace BitCrafts.Infrastructure.Databases;

public sealed class DatabaseManager : IDatabaseManager
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly IDapperWrapper _dapper;
    private readonly ISqlDialectFactory _dialectFactory;

    public DatabaseManager(IDbConnectionFactory connectionFactory, IDapperWrapper dapper, ISqlDialectFactory dialectFactory)
    {
        _connectionFactory = connectionFactory;
        _dapper = dapper;
        _dialectFactory = dialectFactory;
    }

    public async Task<IDbConnection> OpenNewConnection()
    {
        var connection = _connectionFactory.Create() as DbConnection;
        if (connection != null && connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync().ConfigureAwait(false);
        }
        return connection;
    }

    public async Task<IDbTransaction> BeginTransactionAsync()
    {
        var connection = (DbConnection)await OpenNewConnection().ConfigureAwait(false);
        var transaction = await connection.BeginTransactionAsync().ConfigureAwait(false);
        return transaction as DbTransaction;
    }

    public async Task<int> GetLastInsertedIdAsync()
    {
        using (var connection = await OpenNewConnection().ConfigureAwait(false))
        {
            var query = _dialectFactory.Create().GetLastInsertedIdQuery();
            if (string.IsNullOrEmpty(query))
                throw new NotSupportedException("Ce fournisseur de base de données ne prend pas en charge la récupération de l'identifiant inséré.");
            return await _dapper.QuerySingleAsync<int>(connection, query).ConfigureAwait(false);
        }
    }

}