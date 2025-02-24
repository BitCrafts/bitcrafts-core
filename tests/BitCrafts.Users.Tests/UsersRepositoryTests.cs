using BitCrafts.Infrastructure.Abstraction.Databases;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Entities;
using BitCrafts.Users.Repositories;
using NSubstitute;

namespace BitCrafts.Users.Tests;

[TestClass]
public class UsersRepositoryTests
{
    private IDatabaseManager _dbManagerMock;
    private UsersRepository _repository;

    [TestInitialize]
    public void Initialize()
    {
        _dbManagerMock = Substitute.For<IDatabaseManager>();
        _repository = new UsersRepository(_dbManagerMock);
    }

    [TestMethod]
    public async Task AddAsync_ShouldInsertUserAndReturnEntity()
    {
        var user = new User
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "password123",
            PhoneNumber = "555-1234",
            BirthDate = DateTime.UtcNow.AddYears(-30),
            NationalNumber = "123456789",
            PassportNumber = "AB123456"
        };
        _dbManagerMock.GetLastInsertedIdAsync().Returns(1);

        _dbManagerMock.QuerySingleAsync<int>(
            Arg.Any<string>(),
            Arg.Any<object>(),
            null).Returns(1);

        var result = await _repository.AddAsync(user);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
        await _dbManagerMock.Received(1).ExecuteAsync(Arg.Any<string>(), Arg.Any<object>(), null);
        await _dbManagerMock.Received(1).GetLastInsertedIdAsync();
    }

    [TestMethod]
    public async Task GetByIdAsync_ShouldReturnUser()
    {
        var user = new User
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "password123",
            PhoneNumber = "555-1234",
            BirthDate = DateTime.UtcNow.AddYears(-30),
            NationalNumber = "123456789",
            PassportNumber = "AB123456"
        };
        _dbManagerMock.GetLastInsertedIdAsync().Returns(1);
        _dbManagerMock.QuerySingleAsync<IUser>(Arg.Any<string>(), Arg.Any<object>(), null).Returns(user);

        var result = await _repository.GetByIdAsync(1);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
        Assert.AreEqual("John", result.FirstName);
        await _dbManagerMock.Received(1).QuerySingleAsync<IUser>(Arg.Any<string>(), Arg.Any<object>(), null);
    }

    [TestMethod]
    public async Task GetAllAsync_ShouldReturnListOfUsers()
    {
        var users = new List<IUser>
        {
            new User
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123",
                PhoneNumber = "555-1234",
                BirthDate = DateTime.UtcNow.AddYears(-30),
                NationalNumber = "123456789",
                PassportNumber = "AB123456"
            },
            new User
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                Password = "password123",
                PhoneNumber = "555-5678",
                BirthDate = DateTime.UtcNow.AddYears(-25),
                NationalNumber = "987654321",
                PassportNumber = "YZ987654"
            }
        };

        _dbManagerMock.QueryAsync<IUser>(Arg.Any<string>(), null).Returns(users);

        var result = await _repository.GetAllAsync();

        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count());
        await _dbManagerMock.Received(1).QueryAsync<IUser>(Arg.Any<string>(), null, null);
    }

    [TestMethod]
    public async Task UpdateAsync_ShouldReturnTrue_WhenUserIsUpdated()
    {
        var user = new User
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "password123",
            PhoneNumber = "555-1234",
            BirthDate = DateTime.UtcNow.AddYears(-30),
            NationalNumber = "123456789",
            PassportNumber = "AB123456"
        };

        _dbManagerMock.ExecuteAsync(Arg.Any<string>(), Arg.Any<object>(), null).Returns(1);

        var result = await _repository.UpdateAsync(user);

        Assert.IsTrue(result);
        await _dbManagerMock.Received(1).ExecuteAsync(Arg.Any<string>(), Arg.Any<object>(), null);
    }

    [TestMethod]
    public async Task UpdateAsync_ShouldReturnFalse_WhenNoRowsAffected()
    {
        var user = new User
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "password123",
            PhoneNumber = "555-1234",
            BirthDate = DateTime.UtcNow.AddYears(-30),
            NationalNumber = "123456789",
            PassportNumber = "AB123456"
        };

        _dbManagerMock.ExecuteAsync(Arg.Any<string>(), Arg.Any<object>(), null).Returns(0);

        var result = await _repository.UpdateAsync(user);

        Assert.IsFalse(result);
        await _dbManagerMock.Received(1).ExecuteAsync(Arg.Any<string>(), Arg.Any<object>(), null);
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldReturnTrue_WhenUserIsDeleted()
    {
        _dbManagerMock.ExecuteAsync(Arg.Any<string>(), Arg.Any<object>(), null).Returns(1);

        var result = await _repository.DeleteAsync(1);

        Assert.IsTrue(result);
        await _dbManagerMock.Received(1).ExecuteAsync(Arg.Any<string>(), Arg.Any<object>(), null);
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldReturnFalse_WhenNoRowsAffected()
    {
        _dbManagerMock.ExecuteAsync(Arg.Any<string>(), Arg.Any<object>(), null).Returns(0);

        var result = await _repository.DeleteAsync(1);

        Assert.IsFalse(result);
        await _dbManagerMock.Received(1).ExecuteAsync(Arg.Any<string>(), Arg.Any<object>(), null);
    }
}