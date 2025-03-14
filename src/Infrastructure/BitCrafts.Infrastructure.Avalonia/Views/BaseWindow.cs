using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using BitCrafts.Infrastructure.Abstraction.Application.Views;

namespace BitCrafts.Infrastructure.Avalonia.Views;

public abstract class BaseWindow : Window, IView
{
    public bool IsView { get; }
    public bool IsDialog { get; }
    public bool IsWindow { get; }

    public new string Title
    {
        get => base.Title;
        protected set => base.Title = value;
    }

    public bool IsBusy { get; private set; }
    public string BusyText { get; private set; }
    private Grid _rootGrid;
    private TextBlock _windowTitle;
    private Button _closeButton;
    private ScrollContentPresenter _windowContent;

    protected BaseWindow()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
        Initialized += OnInitialized;
        IsDialog = false;
        IsWindow = true;
        IsView = false;
        Title = "UnTitled Window";
        BusyText = string.Empty;
        InitializeControls();
        SetupLayout();
        SetupEvents();
    }

    private void InitializeControls()
    {
        _rootGrid = new Grid
        {
            Name = "RootGrid", VerticalAlignment = VerticalAlignment.Stretch,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        _windowTitle = new TextBlock { Name = "WindowTitle", Text = Title, Margin = new Thickness(5) };
        _closeButton = new Button
        {
            Name = "CloseButton",
            VerticalContentAlignment = VerticalAlignment.Center,
            HorizontalContentAlignment = HorizontalAlignment.Center,
            BorderBrush = Brushes.Green,
            BorderThickness = new Thickness(0),
            Content = "X",
            VerticalAlignment = VerticalAlignment.Top
        };
        _windowContent = new ScrollContentPresenter { Name = "WindowContent" };
    }

    private void SetupLayout()
    {
        // Root Grid
        _rootGrid.RowDefinitions = new RowDefinitions("Auto,*");

        // Header Grid
        var headerGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("*,Auto"),
            Margin = new Thickness(0)
        };

        headerGrid.Children.Add(_windowTitle);
        Grid.SetColumn(_windowTitle, 0);

        // Close Button Border
        var closeButtonBorder = new Border
        {
            Margin = new Thickness(8),
            BorderBrush = Brushes.Green,
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(0),
            Child = _closeButton
        };

        headerGrid.Children.Add(closeButtonBorder);
        Grid.SetColumn(closeButtonBorder, 1);

        _rootGrid.Children.Add(headerGrid);
        Grid.SetRow(headerGrid, 0);

        _rootGrid.Children.Add(_windowContent);
        Grid.SetRow(_windowContent, 1);

        // Border
        var border = new Border
        {
            BorderThickness = new Thickness(1),
            BorderBrush = Brushes.Green,
            Child = _rootGrid
        };

        Content = border;

        // Window properties
        WindowStartupLocation = WindowStartupLocation.CenterOwner;
        SystemDecorations = SystemDecorations.None;
        ShowInTaskbar = true;
        CanResize = false;
        WindowState = WindowState.Maximized;
        Width = 800;
        Height = 450;
    }

    private void SetupEvents()
    {
        _closeButton.Click += CloseButton_OnClick;
    }

    private void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
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