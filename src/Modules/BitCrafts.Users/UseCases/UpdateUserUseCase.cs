using BitCrafts.Infrastructure.Abstraction.Repositories;
using BitCrafts.Infrastructure.Abstraction.UseCases;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Repositories;
using BitCrafts.Users.Abstraction.UseCases;

namespace BitCrafts.Users.UseCases;

public sealed class UpdateUserUseCase : BaseUseCase<UpdateUserEvent, bool> , IUpdateUserUseCase
{
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;

    public UpdateUserUseCase(IRepositoryUnitOfWork repositoryUnitOfWork)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
    }

    private async Task<bool> UpdateUser(UpdateUserEvent message)
    {
        _repositoryUnitOfWork.GetRepository<IUsersRepository>().Update(message.User);
        var result = await _repositoryUnitOfWork.CommitAsync().ConfigureAwait(false);

        return result > 0;
    }


    protected override async Task<bool> ExecuteCore(UpdateUserEvent eventRequest)
    {
        var result = await UpdateUser(eventRequest);
        return result;
    }
}