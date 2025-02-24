using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Finance.Abstraction.Entities;

public interface IBudget : IEntity<int>
{
    string Name { get; init; }
    decimal Limit { get; init; }
    DateTime StartDate { get; init; }
    DateTime EndDate { get; init; }
}