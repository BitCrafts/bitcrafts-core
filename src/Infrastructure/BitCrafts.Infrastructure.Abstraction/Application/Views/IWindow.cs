namespace BitCrafts.Infrastructure.Abstraction.Application.Views;

public interface IWindow : IView
{
    IWindow Owner { get; set; }
    void Close();
}