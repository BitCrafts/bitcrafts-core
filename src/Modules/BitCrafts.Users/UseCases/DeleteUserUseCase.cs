using BitCrafts.Infrastructure.Abstraction.Repositories;
using BitCrafts.Infrastructure.Abstraction.UseCases;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Repositories;
using BitCrafts.Users.Abstraction.UseCases;

namespace BitCrafts.Users.UseCases;

public sealed class DeleteUserUseCase : BaseUseCase<DeleteUserEvent, bool>, IDeleteUserUseCase
{
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;

    public DeleteUserUseCase(IRepositoryUnitOfWork repositoryUnitOfWork)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
    }

    private async Task<bool> DeleteUser(DeleteUserEvent input)
    {
        _repositoryUnitOfWork.GetRepository<IUsersRepository>().Remove(input.User);
        var result = await _repositoryUnitOfWork.CommitAsync().ConfigureAwait(false);

        return result > 0;
    }
    protected override async Task<bool> ExecuteCore(DeleteUserEvent eventRequest)
    {
        var result = await DeleteUser(eventRequest);
        return result;
    }
}