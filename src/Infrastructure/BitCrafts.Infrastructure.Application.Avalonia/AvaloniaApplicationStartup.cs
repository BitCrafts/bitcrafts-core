using System;
using BitCrafts.Infrastructure.Abstraction.Application;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Application.Avalonia;

public sealed class AvaloniaApplicationStartup : BaseApplicationStartup, IApplicationStartup
{
    public AvaloniaApplicationStartup(ILogger<BaseApplicationStartup> logger, IServiceProvider serviceProvider) : base(
        logger, serviceProvider)
    {
    }
}