namespace BitCrafts.Infrastructure.Abstraction.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class PrimaryKeyAttribute : Attribute
{
    public PrimaryKeyAttribute(bool isAutoIncrement = false)
    {
        IsAutoIncrement = isAutoIncrement;
    }

    public bool IsAutoIncrement { get; }
}