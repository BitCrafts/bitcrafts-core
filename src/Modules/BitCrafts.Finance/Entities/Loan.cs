using BitCrafts.Finance.Abstraction.Entities;
using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Finance.Entities;

public class Loan : BaseEntity<int>, ILoan
{
    public decimal Amount { get; set; }
    public decimal InterestRate { get; set; }
    public DateTime StartDate { get; set; }
    public int TermMonths { get; set; }
}