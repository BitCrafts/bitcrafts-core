using System.Data;
using BitCrafts.Core.Contracts.Database;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Npgsql;

namespace BitCrafts.Core.Database;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<DbConnectionFactory> _logger;

    public DbConnectionFactory(IConfiguration configuration, ILogger<DbConnectionFactory> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public IDbConnection Create()
    {
        var connection = GetConnection();
        return connection;
    }

    private IDbConnection GetConnection()
    {
        var databaseProvider = _configuration["DatabaseSettings:Provider"];
        var server = _configuration["DatabaseSettings:Server"];
        var database = _configuration["DatabaseSettings:Database"];
        var user = _configuration["DatabaseSettings:User"];
        var password = _configuration["DatabaseSettings:Password"];

        switch (databaseProvider?.ToLowerInvariant())
        {
            case "sqlite":
                _logger.LogInformation("Using Sqlite database.");
                return new SqliteConnection($"Data Source={database}");

            case "sqlserver":
                _logger.LogInformation("Using SqlServer database.");
                return new SqlConnection(
                    $"Server={server};" +
                    $"Database={database};" +
                    $"User Id={user};" +
                    $"Password={password};"
                );
            case "postgresql":
                _logger.LogInformation("Using PostgreSql database.");
                return new NpgsqlConnection(
                    $"Host={server};" +
                    $"Database={database};" +
                    $"Username={user};" +
                    $"Password={password};"
                );
            case "mariadb":
            case "mysql":
                _logger.LogInformation("Using MariaDB/MySQL database.");
                return new MySqlConnection(
                    $"Server={server};" +
                    $"Database={database};" +
                    $"User={user};" +
                    $"Password={password};"
                );
            default:
                throw new NotSupportedException($"Provider non géré : {databaseProvider}");
        }
    }

    private void SetupDatabase()
    {
        /*var connection =
        ApplicationStartup.IoCContainer.RegisterInstance<IDbConnection>(connection);*/
    }
}