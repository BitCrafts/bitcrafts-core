using BitCrafts.Infrastructure.Abstraction.Databases;

namespace BitCrafts.Infrastructure.Databases;

public class SqliteDialect : ISqlDialect
{
    public string GetLastInsertedIdQuery()
    {
        return "SELECT last_insert_rowid();";
    }

}