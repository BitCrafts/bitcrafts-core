using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.UseCases.Abstraction;
using BitCrafts.Users.Abstraction.Events;

namespace BitCrafts.Users.Abstraction.UseCases;

public interface ICreateUserUseCase : IUseCase<UserCreateEventRequest, UserCreateEventResponse>
{
    
}