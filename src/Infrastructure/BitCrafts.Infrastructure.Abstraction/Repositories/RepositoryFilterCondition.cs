using System.Data;

namespace BitCrafts.Infrastructure.Abstraction.Repositories;

public class RepositoryFilterCondition
{
    public RepositoryFilterCondition(string columnName, string @operator, object value, DbType dbType)
    {
        ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
        Operator = @operator ?? throw new ArgumentNullException(nameof(@operator));
        Value = value;
        DbType = dbType;
    }

    public string ColumnName { get; set; }
    public string Operator { get; set; }
    public object Value { get; set; }
    public DbType DbType { get; set; }
}