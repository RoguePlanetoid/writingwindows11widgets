namespace Widgets.Demo.Desktop;

/// <summary>
/// Main Window
/// </summary>
public sealed partial class MainWindow : Window
{
    private const string icon = "Assets/widgets.ico";

    /// <summary>
    /// Constructor
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        AppWindow.SetIcon(icon);
    }

    /// <summary>
    /// Navigation Loaded
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Navigation_Loaded(object sender, RoutedEventArgs e) =>
        Navigation.SelectedItem = Navigation.MenuItems[0];

    /// <summary>
    /// Navigation Selection Changes
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="args">Event Args</param>
    private void Navigation_SelectionChanged(
        NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        var item = args.SelectedItem as NavigationViewItem;
        var page = item?.Tag switch
        {
            nameof(CustomPage) => typeof(CustomPage),
            nameof(CountsPage) => typeof(CountsPage),
            nameof(PromptPage) => typeof(PromptPage),
            _ => null
        };
        if (page != null)
            ContentFrame.Navigate(page);
    }
}
