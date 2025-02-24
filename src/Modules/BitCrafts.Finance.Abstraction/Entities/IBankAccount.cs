using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Finance.Abstraction.Entities;

public interface IBankAccount : IEntity<int>
{
    int UserId { get; set; }
    string AccountNumber { get; set; }
    decimal Balance { get; set; }
    DateTime CreatedDate { get; set; }
    BankAccountType Type { get; set; }
}