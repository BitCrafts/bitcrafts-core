using System.Data;
using BitCrafts.Customers.Abstraction.Repositories;
using BitCrafts.Customers.Repositories;
using BitCrafts.Infrastructure.Abstraction.Databases;
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Infrastructure.Events;
using BitCrafts.Infrastructure.Threading;
using Microsoft.Data.Sqlite;
using NSubstitute;

namespace BitCrafts.Modules.Tests;

[TestClass]
public abstract class BaseTest
{
    protected ICustomerRepository CustomerRepository;
    protected ICustomerGroupRepository CustomerGroupRepository;

    protected IEventAggregator EventAggregator;

    protected IDbConnectionFactory DbConnectionFactory;

    protected SqliteConnection _sqliteConnection;

    [TestInitialize]
    public virtual void TestInitialize()
    {
        _sqliteConnection = new SqliteConnection("DataSource=:memory:");
        _sqliteConnection.Open();
        DbConnectionFactory = Substitute.For<IDbConnectionFactory>(); 
        DbConnectionFactory.IsMemoryProvider.Returns<bool>(true);
        DbConnectionFactory.Create().Returns<IDbConnection>(_sqliteConnection);
        CustomerRepository = new CustomerRepository(DbConnectionFactory);
        CustomerGroupRepository = new CustomerGroupRepository(DbConnectionFactory);
        EventAggregator = new EventAggregator(new Parallelism());
    }

    [TestCleanup]
    public virtual void TestCleanup()
    {
        EventAggregator = null;
        _sqliteConnection.Close();
    }
}