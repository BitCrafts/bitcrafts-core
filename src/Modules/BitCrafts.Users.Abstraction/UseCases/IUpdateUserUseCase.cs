using BitCrafts.Infrastructure.Abstraction.UseCases;
using BitCrafts.Users.Abstraction.Events;

namespace BitCrafts.Users.Abstraction.UseCases;

public interface IUpdateUserUseCase : IUseCase<UserEventRequest, UserEventResponse>
{
}