using System.Net.Mime;
using BitCrafts.Core;
using BitCrafts.Core.ConsoleApplication;
using BitCrafts.Core.Contracts;
using BitCrafts.Core.Contracts.Applications;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.ConsoleDemo;

class Program
{
    static void Main(string[] args)
    {
        var container = new IoCContainer();

        // Register the container itself for dependency resolution
        container.RegisterInstance<IIoCContainer>(container);

        // Check the argument to decide which application to register
        if (args.Length > 0)
        {
            switch (args[0].ToLower())
            {
                case "console":
                    container.Register<IApplication, ConsoleApplication>(ServiceLifetime.Singleton);
                    break;

                case "web":
                    throw new NotImplementedException();
                    //container.Register<IApplication, WebApplication>(ServiceLifetime.Singleton);
                    break;

                case "gui":
                    throw new NotImplementedException();

                    //container.Register<IApplication, GuiApplication>(ServiceLifetime.Singleton);
                    break;

                default:
                    Console.WriteLine(
                        $"Unknown application type '{args[0]}'. Falling back to default application (Console).");
                    container.Register<IApplication, ConsoleApplication>(ServiceLifetime.Singleton);
                    break;
            }
        }
        else
        {
            // Default application if no arguments are provided
            Console.WriteLine("No arguments provided. Falling back to default application (Console).");
            container.Register<IApplication, ConsoleApplication>(ServiceLifetime.Singleton);
        }

        // Build the IoC container
        container.Build();

        // Resolve the registered application and run it
        var app = container.Resolve<IApplication>();
        app.Run(args);
    }
}