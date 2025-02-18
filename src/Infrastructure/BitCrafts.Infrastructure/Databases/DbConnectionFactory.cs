using System.Data;
using BitCrafts.Infrastructure.Abstraction.Databases;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Databases;

public sealed class DbConnectionFactory : IDbConnectionFactory
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<DbConnectionFactory> _logger;

    public bool IsSqliteProvider { get; private set; }

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
        var configSection = _configuration.GetSection("DatabaseSettings");
        var databaseProvider = configSection.GetValue<string>("Provider");
        var server = configSection.GetValue<string>("Server");
        var database = configSection.GetValue<string>("Database");
        var user = configSection.GetValue<string>("User");
        var password = configSection.GetValue<string>("Password");

        switch (databaseProvider?.ToLowerInvariant())
        {
            case "sqlite":
                IsSqliteProvider = true;
                return new SqliteConnection($"Data Source={database}");

            /*case "sqlserver":
                IsSqlServerProvider = true;
                _logger.LogInformation("Using SqlServer database.");
                return new SqlConnection(
                    $"Server={server};" +
                    $"Database={database};" +
                    $"User Id={user};" +
                    $"Password={password};"
                );
            case "postgresql":
                IsPostgreSqlProvider = true;
                _logger.LogInformation("Using PostgreSql database.");
                return new NpgsqlConnection(
                    $"Host={server};" +
                    $"Database={database};" +
                    $"Username={user};" +
                    $"Password={password};"
                );
            case "mariadb":
            case "mysql":
                IsMySqlProvider = true;
                _logger.LogInformation("Using MariaDB/MySQL database.");
                return new MySqlConnection(
                    $"Server={server};" +
                    $"Database={database};" +
                    $"User={user};" +
                    $"Password={password};"
                );*/
            default:
                throw new NotSupportedException($"Provider not supported : {databaseProvider}");
        }
    }
}