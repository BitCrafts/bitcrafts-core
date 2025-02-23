namespace BitCrafts.Infrastructure.Abstraction.Application;

public interface ISplashScreen : IDisposable
{
    Task ShowAsync();

    void Close();
    
    T GetNativeObject<T>() where T : class;
}