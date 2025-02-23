using BitCrafts.Infrastructure.Abstraction.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Infrastructure.Application;

public sealed class ApplicationFactory : IApplicationFactory
{
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;

    public ApplicationFactory(IConfiguration configuration, IServiceProvider serviceProvider)
    {
        _configuration = configuration;
        _serviceProvider = serviceProvider;
    }

    public IApplication CreateApplication()
    {
        return CreateApplicationFromConfigSection(_configuration.GetSection("ApplicationSettings"));
    }

    private IApplication CreateApplicationFromConfigSection(IConfigurationSection configurationSection)
    {
        var applicationType = configurationSection.GetValue<string>("Type")?.ToLower();
        switch (applicationType)
        {
            default:
                return _serviceProvider.GetRequiredKeyedService<IApplication>("Avalonia");
        }
    }
}