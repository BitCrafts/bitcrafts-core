using BitCrafts.Infrastructure.Abstraction.Databases;
using Microsoft.Extensions.Configuration;

namespace BitCrafts.Infrastructure.Databases;

public class SqlDialectFactory : ISqlDialectFactory
{
    private readonly IConfiguration _configuration;

    public SqlDialectFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ISqlDialect Create()
    {
        return CreateSqlDialect();
    }

    private ISqlDialect CreateSqlDialect()
    {
        var configSection = _configuration.GetSection("DatabaseSettings");
        var databaseProvider = configSection.GetValue<string>("Provider");
        switch (databaseProvider?.ToLowerInvariant())
        {
            case "sqlite":
                return new SqliteDialect();
            case "mariadb":
            case "mysql":
                return new MySqlDialect();
            case "sqlserver":
                return new SqlServerDialect();
            default:
                throw new ApplicationException("Unknown database provider");
        }
    }
}