using BitCrafts.Core.Contracts.Views;

namespace BitCrafts.Core.Contracts.Presenters;

public abstract class BasePresenter<TView> : IPresenter<TView>
    where TView : IView
{
    public TView View { get; private set; }

    protected BasePresenter(TView view)
    {
        View = view ?? throw new ArgumentNullException(nameof(view));
    }

    public virtual void Initialize()
    {
        // Logique de base d'initialisation (peut-être écrasée dans les implémentations concrètes)
    }

    public virtual void OnViewLoaded()
    {
        // Logique à exécuter quand la Vue est chargée
    }

    public virtual void OnViewUnloaded()
    {
        // Logique à exécuter quand la Vue est déchargée
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (View is IDisposable disposableView)
            {
                disposableView.Dispose();
            }
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}