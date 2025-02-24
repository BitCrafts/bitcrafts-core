using BitCrafts.UseCases.Abstraction;
using BitCrafts.Users.Abstraction.Events;

namespace BitCrafts.Users.Abstraction.UseCases;

public interface IDeleteUserUseCase : IUseCase<UserEventRequest, UserEventResponse>
{
    
}