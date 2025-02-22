using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Finance.Abstraction.Entities;

public interface ILoan : IEntity<int>
{
    decimal Amount { get; set; }
    decimal InterestRate { get; set; }
    DateTime StartDate { get; set; }
    int TermMonths { get; set; }

}