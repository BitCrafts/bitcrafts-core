using BitCrafts.Infrastructure.Abstraction.Databases;

namespace BitCrafts.Infrastructure.Databases;

public class MySqlDialect : ISqlDialect
{
    public string GetLastInsertedIdQuery()
    {
        return "SELECT LAST_INSERT_ID();";
    }
}