using System.Data;
using System.Reflection;
using BitCrafts.Infrastructure.Abstraction.Attributes;
using BitCrafts.Infrastructure.Abstraction.Databases;
using BitCrafts.Infrastructure.Abstraction.Entities;
using BitCrafts.Infrastructure.Abstraction.Repositories;
using Dapper;

namespace BitCrafts.Infrastructure.Repositories;

public abstract class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    private readonly IDbConnectionFactory _connectionFactory;

    protected BaseRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
    }

    public async Task<TEntity> GetByIdAsync(TKey id)
    {
        var searchParameter = new RepositorySearchParameter
        {
            Conditions = new List<RepositoryFilterCondition>
            {
                new("Id", "=", id)
            }
        };

        var result = await SearchAsync(searchParameter);
        return result.SingleOrDefault();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var searchParameter = new RepositorySearchParameter
        {
            PageSize = int.MaxValue,
            Page = 1
        };

        return await SearchAsync(searchParameter);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var sql = GetInsertQuery();

        using var connection = _connectionFactory.Create();
        using var transaction = connection.BeginTransaction();

        try
        {
            // Exécute la requête d'insertion
            await connection.ExecuteAsync(sql, entity, transaction);

            // Récupère l'ID en fonction du provider
            var id = await GetLastInsertedIdAsync(connection, transaction);

            transaction.Commit();
            entity.Id = id;
            return entity;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        var sql = GetUpdateQuery();
        using var connection = _connectionFactory.Create();
        var rowAffected = await connection.ExecuteAsync(sql, entity);
        return rowAffected > 0;
    }

    public async Task<bool> DeleteAsync(TKey id)
    {
        var sql = GetDeleteQuery();
        using var connection = _connectionFactory.Create();
        var rowAffected = await connection.ExecuteAsync(sql, new { Id = id });
        return rowAffected > 0;
    }

    public async Task<IEnumerable<TEntity>> SearchAsync(RepositorySearchParameter parameter)
    {
        parameter.Validate();
        var baseQuery = $"SELECT * FROM {GetTableName()} WHERE 1=1";

        var query = BuildSearchWithPagination(baseQuery, parameter) + GetFilters(parameter);
        using var connection = _connectionFactory.Create();
        return await connection.QueryAsync<TEntity>(query, parameter);
    }

    private string BuildSearchWithPagination(string baseQuery, RepositorySearchParameter parameter)
    {
        var query = baseQuery;

        if (parameter.Conditions != null && parameter.Conditions.Any())
            foreach (var condition in parameter.Conditions)
                if (condition.Value == null)
                    query += $" AND {condition.ColumnName} {condition.Operator}";
                else
                    query += $" AND {condition.ColumnName} {condition.Operator} @{condition.ColumnName}";

        if (!string.IsNullOrEmpty(parameter.OrderBy))
            query += $" ORDER BY {parameter.OrderBy} {(parameter.Descending ? "DESC" : "ASC")}";

        if (parameter.Page > 0 && parameter.PageSize > 0)
        {
            var offset = (parameter.Page - 1) * parameter.PageSize;
            query += $" OFFSET {offset} ROWS FETCH NEXT {parameter.PageSize} ROWS ONLY";
        }

        return query;
    }

    protected virtual string GetInsertQuery()
    {
        var properties = typeof(TEntity).GetProperties();
        var primaryKey = GetPrimaryKeyProperty<TEntity>();

        var columns = properties
            .Where(p => p != primaryKey || !IsPrimaryKeyAutoIncrement<TEntity>())
            .Select(p => p.Name)
            .ToList();

        var values = columns.Select(c => $"@{c}").ToList();

        var tableName = GetTableName();
        var columnPart = string.Join(", ", columns);
        var valuesPart = string.Join(", ", values);

        return $"INSERT INTO {tableName} ({columnPart}) VALUES ({valuesPart})";
    }

    protected virtual string GetUpdateQuery()
    {
        var keyProperty = typeof(TEntity).GetProperties()
                              .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(PrimaryKeyAttribute)))
                          ?? throw new InvalidOperationException(
                              $"No primary key defined for the entity {typeof(TEntity).Name}.");

        var keyPropertyName = keyProperty.Name;

        var setClause = string.Join(", ", typeof(TEntity).GetProperties()
            .Where(prop => prop.Name != keyPropertyName)
            .Select(prop => $"{prop.Name} = @{prop.Name}"));

        return $"UPDATE {GetTableName()} SET {setClause} WHERE {keyPropertyName} = @{keyPropertyName}";
    }

    protected virtual string GetDeleteQuery()
    {
        var keyProperty = typeof(TEntity).GetProperties()
                              .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(PrimaryKeyAttribute)))
                          ?? throw new InvalidOperationException(
                              $"No primary key defined for the entity {typeof(TEntity).Name}.");

        var keyPropertyName = keyProperty.Name;

        return $"DELETE FROM {GetTableName()} WHERE {keyPropertyName} = @{keyPropertyName}";
    }

    protected virtual string GetTableName()
    {
        return typeof(TEntity).Name;
    }

    protected virtual string GetFilters(RepositorySearchParameter parameter)
    {
        return string.Empty;
    }

    private async Task<TKey> GetLastInsertedIdAsync(IDbConnection connection, IDbTransaction transaction)
    {
        if (_connectionFactory.IsSqliteProvider)
            return await connection.ExecuteScalarAsync<TKey>("SELECT last_insert_rowid();", transaction: transaction);

        /*if (_connectionFactory.IsSqlServerProvider)
            return await connection.ExecuteScalarAsync<TKey>("SELECT CAST(SCOPE_IDENTITY() as bigint);",
                transaction: transaction);

        if (_connectionFactory.IsMySqlProvider)
            return await connection.ExecuteScalarAsync<TKey>("SELECT LAST_INSERT_ID();", transaction: transaction);*/

        throw new NotSupportedException("Database provider not supported.");
    }

    public PropertyInfo GetPrimaryKeyProperty<T>()
    {
        var properties = typeof(T).GetProperties();

        var primaryKeyProperty = properties.SingleOrDefault(prop =>
            Attribute.IsDefined(prop, typeof(PrimaryKeyAttribute)));

        if (primaryKeyProperty == null)
            throw new InvalidOperationException($"No primary key defined for type {typeof(T).Name}.");

        return primaryKeyProperty;
    }

    public bool IsPrimaryKeyAutoIncrement<T>()
    {
        var primaryKey = GetPrimaryKeyProperty<T>();

        var attribute = primaryKey.GetCustomAttribute<PrimaryKeyAttribute>();
        return attribute?.IsAutoIncrement ?? false;
    }
}