using System.Data;
using BitCrafts.Modules.Users.Contracts.Models;
using BitCrafts.Modules.Users.Contracts.Repositories;
using BitCrafts.Modules.Users.Models;
using Dapper;

namespace BitCrafts.Modules.Users.Repositories;

public sealed class UsersRepository : IUsersRepository
{
    private readonly IDbConnection _connection;

    public UsersRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public IEnumerable<IUserEntity> GetAllUsers()
    {
        if (_connection.State != ConnectionState.Open)
            _connection.Open();

        const string sql = @"SELECT PrimaryKey ,
                                    FirstName,
                                    LastName,
                                    Email,
                                    Phone
                             FROM Users;"; // À adapter

        var result = _connection.Query<UserEntity>(sql);
        _connection.Close();
        return result;
    }

    public void AddUser(IUserEntity user)
    {
        if (_connection.State != ConnectionState.Open)
            _connection.Open();
        const string insertSql = @"INSERT INTO Users
                                   (FirstName, LastName, Email, Phone)
                                   VALUES
                                   (@FirstName, @LastName, @Email, @Phone);
                                   SELECT LAST_INSERT_ROWID();";

        // On récupère la clé primaire auto-générée, si nécessaire
        var newId = _connection.ExecuteScalar<int>(
            insertSql,
            new { user.FirstName, user.LastName, user.Email, user.Phone }
        );

        user.PrimaryKey = newId;
        _connection.Close();
    }

    public void DeleteUser(int id)
    {
        if (_connection.State != ConnectionState.Open)
            _connection.Open();
        const string deleteSql = @"DELETE FROM Users WHERE PrimaryKey = @Id";
        _connection.Execute(deleteSql, new { Id = id });
        _connection.Close();
    }
}