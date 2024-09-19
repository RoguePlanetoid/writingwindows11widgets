namespace Widgets.Demo.Desktop;

/// <summary>
/// Provides application-specific behaviour to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    private const string headless = "-headless";
    private static ICustomProvider? _custom;
    private static IPromptProvider? _prompt;
    private static IPipeProvider? _pipe;

    /// <summary>
    /// Host
    /// </summary>
    public static IHost? Host { get; private set; }

    /// <summary>
    /// Activate Main Window
    /// </summary>
    private static void ActivateMainWindow() =>
        Host?.Services.GetRequiredService<MainWindow>()?.Activate();

    /// <summary>
    /// Start Service Host
    /// </summary>
    private static async Task StartServiceHost()
    {
        Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
        .ConfigureServices(services => services.AddServices())
        .Build();
        await Host!.StartAsync();
        _custom = Host?.Services.GetRequiredService<ICustomProvider>();
        _prompt = Host?.Services.GetRequiredService<IPromptProvider>();
        _pipe = Host?.Services.GetRequiredService<IPipeProvider>();
    }

    /// <summary>
    /// Is Headless?
    /// </summary>
    /// <returns>True if Is, False if Not</returns>
    private static bool IsHeadless() =>
        Environment.GetCommandLineArgs().Contains(headless);

    /// <summary>
    /// Register Widget Provider
    /// </summary>
    private static void RegisterWidgetProvider()
    {
        ComWrappersSupport.InitializeComWrappers();
        WidgetProvider.AddWidget(CustomWidget.DefinitionId, (widgetId, initialState) =>
            new CustomWidget(_custom, widgetId, initialState));
        WidgetProvider.AddWidget(CountsWidget.DefinitionId, (widgetId, initialState) =>
            new CountsWidget(_pipe, widgetId, initialState));
        WidgetProvider.AddWidget(PromptWidget.DefinitionId, (widgetId, initialState) =>
            new PromptWidget(_prompt, widgetId, initialState));
        WidgetRegistrationManager<WidgetProvider>.RegisterProvider();
    }

    /// <summary>
    /// Set Widget State
    /// </summary>
    /// <param name="model">Widget Model</param>
    public static void SetWidgetState(WidgetModel model)
    {
        var widgetIds = WidgetManager.GetDefault().GetWidgetIds();
        if (widgetIds != null)
        {
            foreach (var widgetId in widgetIds.Where(widgetId => widgetId != null))
            {
                if (WidgetManager.GetDefault().GetWidgetInfo(widgetId)
                    ?.WidgetContext?.DefinitionId == model.DefinitionId)
                    WidgetProvider.UpdateWidget(widgetId, model.State);
            }
        }
    }

    /// <summary>
    /// Get Widget State
    /// </summary>
    /// <param name="definitionId">Definition Id</param>
    /// <returns>Widget State</returns>
    public static string? GetWidgetState(string definitionId)
    {
        var widgetIds = WidgetManager.GetDefault().GetWidgetIds();
        return widgetIds?.Select(widgetId =>
            WidgetManager.GetDefault().GetWidgetInfo(widgetId))
                .SingleOrDefault(info => info?.WidgetContext?.DefinitionId ==
                    definitionId)?.CustomState;
    }

    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App() => InitializeComponent();

    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
        await StartServiceHost();
        RegisterWidgetProvider();
        if (!IsHeadless())
        {
            ActivateMainWindow();
            _pipe?.Server(CountsWidget.DefinitionId);
        }
        base.OnLaunched(args);
    }
}
