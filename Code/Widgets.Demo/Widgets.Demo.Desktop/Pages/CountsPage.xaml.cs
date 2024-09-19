namespace Widgets.Demo.Desktop.Pages;

/// <summary>
/// Counts Page
/// </summary>
public sealed partial class CountsPage : Page
{
    private readonly ICountsProvider? _provider;

    /// <summary>
    /// Constructor
    /// </summary>
    public CountsPage()
    {
        InitializeComponent();
        _provider = App.Host?.Services.GetService<ICountsProvider>();
        Display.DataContext = _provider!.ViewModel;
        _provider!.Updated += (object? sender, WidgetEventArgs e) =>
            App.SetWidgetState(e.Model);
    }

    /// <summary>
    /// On Navigated To
    /// </summary>
    /// <param name="e">Event Args</param>
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        _provider!.SetInitialState(App.GetWidgetState);
        base.OnNavigatedTo(e);
    }
}
