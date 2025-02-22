using System.Data;

namespace BitCrafts.Infrastructure.Abstraction.Databases;

public interface IDatabaseManager
{
    Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null);
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null);
    Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null);

    Task<int> GetLastInsertedIdAsync();

}