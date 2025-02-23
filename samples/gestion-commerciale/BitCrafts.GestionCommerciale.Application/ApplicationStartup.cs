using BitCrafts.Infrastructure.Abstraction.Application;

namespace BitCrafts.GestionCommerciale.Application;

public sealed class ApplicationStartup : IApplicationStartup
{
    public ApplicationStartup()
    {
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }

    public Task StartAsync()
    {
        return Task.CompletedTask;
    }
}