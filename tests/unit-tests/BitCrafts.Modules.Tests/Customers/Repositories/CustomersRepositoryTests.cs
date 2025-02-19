using BitCrafts.Customers.Entities;
using Dapper;

namespace BitCrafts.Modules.Tests.Customers.Repositories;

[TestClass]
[TestCategory("Customers Repositories")]
public sealed class CustomersRepositoryTests : BaseTest
{
    private void CreateSchema()
    {
        var createCustomerTableSql = @"
                CREATE TABLE Customer (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    FirstName TEXT NOT NULL,
                    LastName TEXT NOT NULL,
                    Email TEXT NOT NULL,
                    Phone TEXT NOT NULL,
                    GroupId INTEGER NULL
                );
            ";

        var createCustomerGroupTableSql = @"
                CREATE TABLE CustomerGroup (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL
                );
            ";
        _sqliteConnection.Execute(createCustomerTableSql);
        _sqliteConnection.Execute(createCustomerGroupTableSql);
    }

    private void SeedTables()
    {
        _sqliteConnection.Execute(
            "INSERT INTO Customer VALUES(1,'First','First','test@example.com','003365987412',null);");
        _sqliteConnection.Execute("INSERT INTO CustomerGroup  VALUES (1,'Default');");
    }

    [TestInitialize]
    public override void TestInitialize()
    {
        base.TestInitialize();
        CreateSchema();
        SeedTables();
    }

    [TestMethod]
    public async Task CustomerRepository_ShouldReturnCustomerById()
    {
        // Act
        var result = await CustomerRepository.GetByIdAsync<Customer>(1);

        // Assert
        Assert.IsNotNull(result, "Customer should not be null");
    }
/*
    [TestMethod]
    public async Task AssignCustomerToGroup_ShouldReturnTrue_WhenRowAffectedIsGreaterThanZero()
    {
        int customerId = 1;
        int groupId = 100;

        // Act
        bool result = await CustomerRepository.AssignCustomerToGroup(customerId, groupId);

        // Assert
        Assert.IsTrue(result, "La méthode devrait renvoyer true lorsque des lignes sont modifiées.");
    }

    [TestMethod]
    public async Task AssignCustomerToGroup_ShouldReturnFalse_WhenNoRowIsAffected()
    {
        int customerId = 2;
        int groupId = 200;

        // Act
        bool result = await CustomerRepository.AssignCustomerToGroup(customerId, groupId);

        // Assert
        Assert.IsFalse(result, "La méthode doit renvoyer false lorsque Aucune ligne n'est affectée.");
    }*/
    /*[TestMethod]
  public async Task AddAsync_ShouldReturnEntityWithNewId()
   {
       // Arrange
       var customer = new Customer()
       {
           FirstName = "Test",
           LastName = "Test",
           Email = "<EMAIL>",
           Phone = "0123456789",
           GroupId = 0
       };

       // Act
       var addedCustomer = await CustomerRepository.AddAsync(customer);

       // Assert
       Assert.IsNotNull(addedCustomer);
       Assert.IsTrue(addedCustomer.Id > 0, "L'identifiant généré doit être supérieur à 0.");

       // Vérification via GetByIdAsync
       var fetched = await CustomerRepository.GetByIdAsync(addedCustomer.Id);
       Assert.IsNotNull(fetched);
       Assert.AreEqual(customer.FirstName, fetched.FirstName);
       Assert.AreEqual(customer.LastName, fetched.LastName);
   }*/
}