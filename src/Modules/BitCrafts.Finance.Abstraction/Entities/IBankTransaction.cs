using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Finance.Abstraction.Entities;

public interface IBankTransaction : IEntity<int>
{
    int AccountId { get; set; }
    decimal Amount { get; set; }
    DateTime Date { get; set; }
    BankTransactionType Type { get; set; }
}