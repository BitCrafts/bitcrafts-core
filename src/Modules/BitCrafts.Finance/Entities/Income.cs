using BitCrafts.Finance.Abstraction.Entities;
using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Finance.Entities;

public class Income : BaseEntity<int>, IIncome
{
    public int TransactionId { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}