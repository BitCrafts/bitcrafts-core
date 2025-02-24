using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Finance.Abstraction.Entities;

public interface IExpense : IEntity<int>
{
    int TransactionId { get; set; }
    string Name { get; set; }
    decimal Amount { get; set; }
    DateTime Date { get; set; }
}