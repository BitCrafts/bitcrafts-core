using BitCrafts.Infrastructure.Abstraction.Databases;

namespace BitCrafts.Infrastructure.Databases;

public class SqlServerDialect : ISqlDialect
{
    public string GetLastInsertedIdQuery()
    {
        return "SELECT SCOPE_IDENTITY();";
    }
}