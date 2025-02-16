using BitCrafts.Core.ConsoleApplication.Presenters;
using BitCrafts.Core.Contracts.Applications;

namespace BitCrafts.Core.ConsoleApplication;

public sealed class ConsoleNativeApplication : INativeApplication
{
    private readonly IMainPresenter _mainPresenter;
    private bool _isRunning;

    public ConsoleNativeApplication(IMainPresenter mainPresenter)
    {
        _mainPresenter = mainPresenter;
        Console.WriteLine("Application Console démarrée. Appuyez sur 'q' pour quitter.");
        _mainPresenter.Initialize();
    }

    public void Run()
    {
        _isRunning = true;

        while (_isRunning)
        {
            var key = Console.ReadKey(true);
            if (key.KeyChar == 'q')
            {
                Shutdown();
            }
        }

        Console.WriteLine("Application Console arrêtée.");
    }

    public void Shutdown()
    {
        _isRunning = false;
    }

    public void Dispose()
    {
        Shutdown();
    }
}