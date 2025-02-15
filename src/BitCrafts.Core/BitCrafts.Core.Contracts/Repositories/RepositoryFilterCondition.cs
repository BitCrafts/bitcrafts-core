namespace BitCrafts.Core.Contracts.Repositories;

public class RepositoryFilterCondition
{
    public RepositoryFilterCondition(string columnName, string @operator, object? value = null)
    {
        ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
        Operator = @operator ?? throw new ArgumentNullException(nameof(@operator));
        Value = value;
    }

    public string ColumnName { get; set; }
    public string Operator { get; set; }
    public object Value { get; set; }
}