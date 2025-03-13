namespace BitCrafts.Infrastructure.Abstraction.UseCases;

public interface IUseCase<TInput> : IDisposable
{
    Task Execute(TInput input);
}

public interface IUseCase : IDisposable
{
    Task Execute();
}