namespace BitCrafts.Infrastructure.Abstraction.Application.Managers;

public interface IUiManager : IDisposable
{
    Task ShutdownAsync();
}