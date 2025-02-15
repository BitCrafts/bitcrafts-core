using System.Reflection;
using BitCrafts.Core.Contracts.Attributes;

namespace BitCrafts.Core.Contracts.Helpers;

internal static class EntityHelper
{
    public static PropertyInfo GetPrimaryKeyProperty<T>()
    {
        var properties = typeof(T).GetProperties();

        var primaryKeyProperty = properties.SingleOrDefault(prop =>
            Attribute.IsDefined(prop, typeof(PrimaryKeyAttribute)));

        if (primaryKeyProperty == null)
            throw new InvalidOperationException($"No primary key defined for type {typeof(T).Name}.");

        return primaryKeyProperty;
    }

    public static bool IsPrimaryKeyAutoIncrement<T>()
    {
        var primaryKey = GetPrimaryKeyProperty<T>();

        var attribute = primaryKey.GetCustomAttribute<PrimaryKeyAttribute>();
        return attribute?.IsAutoIncrement ?? false;
    }
}