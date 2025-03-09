using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.Infrastructure.Abstraction.UseCases;

public interface IUseCase<TInput, TOutput> : IDisposable
{
    Task<TOutput> Execute(TInput eventRequest);
}