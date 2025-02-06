using System.Net.Mime;
using BitCrafts.Core;
using BitCrafts.Core.ConsoleApplication;
using BitCrafts.Core.Contracts;
using BitCrafts.Core.Contracts.Applications;
using BitCrafts.Core.GtkApplication;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.ConsoleDemo;

internal class Program
{
    private static void Main(string[] args)
    {
        IApplication app = null;
        if (args.Length > 0)
        {
            switch (args[0].ToLower())
            {
                case "console":
                    app = new ConsoleApplication();
                    break;

                case "web":
                    throw new NotImplementedException();
                    //container.Register<IApplication, WebApplication>(ServiceLifetime.Singleton);
                    break;

                case "gui":
                    app = new GtkApplication();
                    break;

                default:
                    Console.WriteLine(
                        $"Unknown application type '{args[0]}'. Falling back to default application (Console).");
                    app = new ConsoleApplication();
                    break;
            }
        }

        if (app != null) app.Run(args);
    }
}