using System.Data;

namespace BitCrafts.Core.Contracts.Database;

public interface IDbConnectionFactory
{
    bool IsSqliteProvider { get; }

    bool IsSqlServerProvider { get; }

    bool IsMySqlProvider { get; }

    bool IsPostgreSqlProvider { get; }
    IDbConnection Create();
}