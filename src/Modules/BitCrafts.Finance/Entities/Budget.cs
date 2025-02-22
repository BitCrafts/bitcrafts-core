using BitCrafts.Finance.Abstraction.Entities;
using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Finance.Entities;

public class Budget : BaseEntity<int>,IBudget
{
    public string Name { get; init; }
    public decimal Limit { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }

}