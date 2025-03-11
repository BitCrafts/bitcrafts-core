using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Loader;
using BitCrafts.Infrastructure.Abstraction.Modules;
using BitCrafts.Infrastructure.Modules;
using BitCrafts.Module.TestModule;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Serilog;

namespace BitCrafts.InfrastructureTests;

[TestClass]
public class ModuleRegistrerTests
{
    private ILogger _logger;
    private ModuleRegistrer _moduleRegistrer;
    private IServiceCollection _services;
    private string _testModulesPath;
    private string _currentPath;

    [TestInitialize]
    public void Setup()
    {
        _logger = Substitute.For<ILogger>();
        _services = new ServiceCollection();
        _moduleRegistrer = new ModuleRegistrer(_logger);

        _currentPath = Directory.GetCurrentDirectory();
        _testModulesPath = Path.Combine(_currentPath, "Modules");

        if (!Directory.Exists(_testModulesPath))
        {
            Directory.CreateDirectory(_testModulesPath);
        }
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (Directory.Exists(_testModulesPath))
        {
            Directory.Delete(_testModulesPath, true);
        }

        _moduleRegistrer.Dispose();
    }

    [TestMethod]
    public void RegisterModules_LoadsAndRegistersModules()
    {
        _moduleRegistrer.RegisterModules(_services);

        _logger.Received().Information(Arg.Any<string>());
        _logger.Received().Information(Arg.Any<string>());
        var provider = _services.BuildServiceProvider();
        Assert.IsNotNull(provider.GetService<ITestModule>());
        Assert.IsNotNull(provider.GetService<TestModule>());
        provider.Dispose();
    }

    

    public class NotAModule
    {
    }
}