using BitCrafts.Finance.Abstraction.Entities;
using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Finance.Entities;

public class BankAccount : BaseEntity<int>, IBankAccount
{
    public int UserId { get; set; }
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedDate { get; set; }
    public BankAccountType Type { get; set; }
}