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
    // Références aux repositories qui utilisent une connexion "in memory"
    protected ICustomerRepository CustomerRepository;
    protected ICustomerGroupRepository CustomerGroupRepository;

    // Exemple d'EventAggregator (à remplacer par votre implémentation ou interface réelle)
    protected IEventAggregator EventAggregator;

    // Fake IDbConnectionFactory pour simuler la création de connexion
    protected IDbConnectionFactory DbConnectionFactory;

    protected SqliteConnection _sqliteConnection = new SqliteConnection("Data Source=:memory:");

    [TestInitialize]
    public virtual void TestInitialize()
    {
        _sqliteConnection.Open();
        DbConnectionFactory = Substitute.For<IDbConnectionFactory>();
        DbConnectionFactory.Create().Returns<IDbConnection>(_sqliteConnection);
        CustomerRepository = new CustomerRepository(DbConnectionFactory);
        CustomerGroupRepository = new CustomerGroupRepository(DbConnectionFactory);
        EventAggregator = new EventAggregator(new Parallelism());
    }

    [TestCleanup]
    public virtual void TestCleanup()
    {
        EventAggregator = null;
    }
}