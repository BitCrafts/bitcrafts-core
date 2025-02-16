using BitCrafts.Core.ConsoleApplication.Views;

namespace BitCrafts.Core.ConsoleApplication.Presenters;

public class ConsoleMainPresenter : IMainPresenter
{
    public ConsoleMainPresenter(IMainView view, IMainPresenterModel model)
    {
        View = view;
        Model = model;
    }


    public IMainView View { get; }
    public IMainPresenterModel Model { get; }
    
    public void Initialize()
    {
        throw new NotImplementedException();
    }
}