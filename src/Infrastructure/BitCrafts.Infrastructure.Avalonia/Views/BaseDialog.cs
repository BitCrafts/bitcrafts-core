using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using BitCrafts.Infrastructure.Abstraction.Application.Views;

namespace BitCrafts.Infrastructure.Avalonia.Views;

public abstract class BaseDialog : Window, IView
{
    private TextBlock _windowTitle;
    private Button _closeButton;
    private ScrollContentPresenter _dialogContent;
    public bool IsView { get; }
    public bool IsDialog { get; }
    public bool IsWindow { get; }
    public bool IsBusy { get; private set; }
    public string BusyText { get; private set; }

    public new string Title
    {
        get => base.Title;
        protected set => base.Title = value;
    }

    private void InitializeControls()
    {
        _windowTitle = new TextBlock { Name = "WindowTitle", Text = Title, Margin = new Thickness(5) };
        _closeButton = new Button
        {
            Name = "CloseButton",
            BorderBrush = Brushes.Green,
            BorderThickness = new Thickness(0),
            HorizontalAlignment = HorizontalAlignment.Right,
            Padding = new Thickness(5),
            VerticalAlignment = VerticalAlignment.Top,
            CornerRadius = new CornerRadius(0),
            Content = "X",
            Width = 32,
            Height = 32
        };
        _dialogContent = new ScrollContentPresenter { Name = "DialogContent" };
    }

    private void SetupLayout()
    {
        var mainGrid = new Grid
        {
            RowDefinitions = new RowDefinitions("Auto,*,Auto")
        };

        var headerGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("*,Auto"),
            Margin = new Thickness(0)
        };

        headerGrid.Children.Add(_windowTitle);
        Grid.SetColumn(_windowTitle, 0);

        headerGrid.Children.Add(_closeButton);
        Grid.SetColumn(_closeButton, 1);

        mainGrid.Children.Add(headerGrid);
        Grid.SetRow(headerGrid, 0);

        mainGrid.Children.Add(_dialogContent);
        Grid.SetRow(_dialogContent, 1);

        var border = new Border
        {
            BorderThickness = new Thickness(1),
            BorderBrush = Brushes.Green,
            Child = mainGrid
        };

        Content = border;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        CanResize = false;
        ShowInTaskbar = false;
        SystemDecorations = SystemDecorations.None;
        MinWidth = 450;
        MinHeight = 250;
    }

    private void SetupEvents()
    {
        _closeButton.Click += CloseButton_OnClick;
    }

    private void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    protected BaseDialog()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
        Initialized += OnInitialized;
        IsDialog = true;
        IsWindow = false;
        IsView = false;
        Title = "UnTitled Control";
        BusyText = string.Empty;
        InitializeControls();
        SetupLayout();
        SetupEvents();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        OnDisappearing();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        OnAppeared();
    }

    private void OnInitialized(object sender, EventArgs e)
    {
        OnAppearing();
    }

    protected abstract void OnAppearing();
    protected abstract void OnAppeared();
    protected abstract void OnDisappearing();


    public void SetBusy(string message)
    {
        IsBusy = true;
        BusyText = message;
    }

    public void UnsetBusy()
    {
        IsBusy = false;
        BusyText = string.Empty;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }


    protected virtual void Dispose(bool disposing)
    {
        if (!disposing) return;
        Loaded -= OnLoaded;
        Unloaded -= OnUnloaded;
        Initialized -= OnInitialized;
    }
}