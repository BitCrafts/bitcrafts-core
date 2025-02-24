namespace BitCrafts.Infrastructure.Abstraction.Application;

public interface ISplashScreen : IDisposable
{
    void SetText(string text);
    Task ShowAsync();

    void Close();

    T GetNativeObject<T>() where T : class;
}