using BitCrafts.Infrastructure.Abstraction.Databases;
using BitCrafts.Inventory.Abstraction.Entities;
using BitCrafts.Inventory.Abstraction.Repositories;

namespace BitCrafts.Inventory.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private readonly IDatabaseManager _dbManager;

    public InventoryRepository(IDatabaseManager dbManager)
    {
        _dbManager = dbManager;
    }

    public async Task<IInventory> AddAsync(IInventory entity)
    {
        await _dbManager.ExecuteAsync(
            "INSERT INTO Inventory (Name, Description, Location) VALUES (@Name, @Description, @Location);",
            entity);

        var id = await _dbManager.GetLastInsertedIdAsync();
        entity.Id = id;
        return entity;
    }

    public async Task<IEnumerable<IInventory>> GetAllAsync()
    {
        return await _dbManager.QueryAsync<IInventory>("SELECT * FROM Inventory;");
    }

    public async Task<IInventory> GetByIdAsync(int id)
    {
        return await _dbManager.QuerySingleAsync<IInventory>(
            "SELECT * FROM Inventory WHERE Id = @Id;", 
            new { Id = id });
    }

    public async Task<bool> UpdateAsync(IInventory entity)
    {
        var result = await _dbManager.ExecuteAsync(
            "UPDATE Inventory SET Name = @Name, Description = @Description, Location = @Location WHERE Id = @Id;",
            entity);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _dbManager.ExecuteAsync(
            "DELETE FROM Inventory WHERE Id = @Id;", 
            new { Id = id });

        return result > 0;
    }
    
}