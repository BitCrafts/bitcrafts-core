using BitCrafts.Finance.Abstraction.Entities;
using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Finance.Entities;

public class BankTransaction : BaseEntity<int>, IBankTransaction
{
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public BankTransactionType Type { get; set; }
}