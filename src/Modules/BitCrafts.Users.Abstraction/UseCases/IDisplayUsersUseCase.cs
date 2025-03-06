using BitCrafts.UseCases.Abstraction;
using BitCrafts.Users.Abstraction.Events;

namespace BitCrafts.Users.Abstraction.UseCases;

public interface IDisplayUsersUseCase : IUseCase<DisplayUsersEventRequest, DisplayUsersEventResponse>
{
    
}