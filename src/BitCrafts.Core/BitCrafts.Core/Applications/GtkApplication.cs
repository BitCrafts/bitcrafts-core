using System.Data;
using BitCrafts.Core.Contracts.Applications;
using BitCrafts.Core.Presenters;
using BitCrafts.Core.Views;
using Gio;
using Gtk;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using Npgsql;
using Application = Gtk.Application;
using Task = System.Threading.Tasks.Task;

namespace BitCrafts.Core.Applications;

public class GtkApplication : BaseApplication, IGtkApplication
{
    private Application _app;

    public override Task InitializeAsync(CancellationToken cancellationToken)
    {
        ApplicationStartup.IoCContainer.Register<IMainWindowView, MainWindowView>(ServiceLifetime.Singleton);
        ApplicationStartup.IoCContainer.Register<IMainPresenter, MainPresenter>(ServiceLifetime.Singleton);
        return Task.CompletedTask;
    }

    public override async Task RunAsync()
    {
        SetupDatabase();
        ApplicationStartup.IoCContainer.Build();
        RunApp();
        await Task.FromResult(0);
    }

    private void RunApp()
    {
        var appId = ApplicationStartup.Configuration["ApplicationSettings:Id"];
        _app = Application.New(appId, ApplicationFlags.DefaultFlags);
        _app.OnActivate += (o, e) =>
        {
            if (ApplicationStartup.IoCContainer.Resolve<IMainWindowView>() is Window window)
            {
                window.Title = ApplicationStartup.Configuration["ApplicationSettings:Name"];
                window.OnDestroy += WindowOnDestroy;
                _app.AddWindow(window);
                if (window is IMainWindowView view)
                {
                    view.InitializeView();
                    view.ShowView();
                }
            }
        };
        _app.Run(0, null);
    }

    private void SetupDatabase()
    {
        var databaseProvider = ApplicationStartup.Configuration["DatabaseSettings:Provider"];
        var server = ApplicationStartup.Configuration["DatabaseSettings:Server"];
        var database = ApplicationStartup.Configuration["DatabaseSettings:Database"];
        var user = ApplicationStartup.Configuration["DatabaseSettings:User"];
        var password = ApplicationStartup.Configuration["DatabaseSettings:Password"];

        var connection = GetConnection(databaseProvider, server, database, user, password);
        ApplicationStartup.IoCContainer.RegisterInstance<IDbConnection>(connection);
    }

    private IDbConnection GetConnection(string databaseProvider, string server, string database, string user,
        string password)
    {
        switch (databaseProvider?.ToLowerInvariant())
        {
            case "sqlite":
                ApplicationStartup.Logger.Information("Using Sqlite database.");
                return new SqliteConnection($"Data Source={database}");

            case "sqlserver":
                ApplicationStartup.Logger.Information("Using SqlServer database.");
                return new SqlConnection(
                    $"Server={server};" +
                    $"Database={database};" +
                    $"User Id={user};" +
                    $"Password={password};"
                );
            case "postgresql":
                ApplicationStartup.Logger.Information("Using PostgreSql database.");
                return new NpgsqlConnection(
                    $"Host={server};" +
                    $"Database={database};" +
                    $"Username={user};" +
                    $"Password={password};"
                );
            case "mariadb":
            case "mysql":
                ApplicationStartup.Logger.Information("Using MariaDB/MySQL database.");
                return new MySqlConnection(
                    $"Server={server};" +
                    $"Database={database};" +
                    $"User={user};" +
                    $"Password={password};"
                );
            default:
                throw new NotSupportedException($"Provider non géré : {databaseProvider}");
        }
    }

    public override Task ShutdownAsync(CancellationToken cancellationToken)
    {
        if (_app != null)
            _app.Quit();
        return Task.CompletedTask;
    }

    private void WindowOnDestroy(Widget sender, EventArgs args)
    {
        ShutdownAsync(CancellationToken.None).Wait();
    }
}