namespace BitCrafts.Infrastructure.Abstraction.Application.Managers;

public interface IExceptionManager
{
    void HandleException(Exception exception);
}