using System.Data;

namespace BitCrafts.Core.Contracts.Database;

public interface IDbConnectionFactory
{
    IDbConnection Create();
}