namespace Widgets.Demo.Desktop.Pages;

/// <summary>
/// Custom Page
/// </summary>
public sealed partial class CustomPage : Page
{
    private readonly ICustomProvider? _provider;

    /// <summary>
    /// Constructor
    /// </summary>
    public CustomPage()
    {
        InitializeComponent();
        _provider = App.Host?.Services.GetService<ICustomProvider>();
        Display.DataContext = _provider!.ViewModel;
        _provider!.Updated += (object? sender, WidgetEventArgs e) =>
            App.SetWidgetState(e.Model);
    }
}
