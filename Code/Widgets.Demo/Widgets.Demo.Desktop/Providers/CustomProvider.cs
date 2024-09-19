namespace Widgets.Demo.Desktop.Providers;

/// <summary>
/// Custom Provider
/// </summary>
internal class CustomProvider : ICustomProvider
{
    private const string name = "custom.json";
    private const string data = "ms-appx:///Widgets/Data/WeatherWidgetData.json";
    private const string template = "ms-appx:///Widgets/Templates/WeatherWidgetTemplate.json";
    private static readonly string defaultTemplate = WidgetHelper.ReadJsonFromPackage(template);
    private static readonly string defaultData = WidgetHelper.ReadJsonFromPackage(data);
    private readonly IFileProvider _provider;

    /// <summary>
    /// Get Default Widget
    /// </summary>
    /// <returns>Widget Model</returns>
    private static WidgetModel GetDefaultWidget() => new()
    {
        DefinitionId = CustomWidget.DefinitionId,
        Template = defaultTemplate,
        Data = defaultData
    };

    /// <summary>
    /// Save
    /// </summary>
    /// <param name="model">Widget Model</param>
    /// <returns>True on Success, False if Not</returns>
    private async Task<bool> SaveAsync(WidgetModel? model)
    {
        if (model == null)
            return false;
        var success = await _provider.SaveAsync(name, model);
        if (success)
            Updated?.Invoke(this, new WidgetEventArgs(model));
        return success;
    }

    /// <summary>
    /// Reset
    /// </summary>
    private async Task ResetAsync()
    {
        if(await SaveAsync(GetDefaultWidget()))
            ViewModel.Model = GetDefaultWidget();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="provider">File Provider</param>
    public CustomProvider(IFileProvider provider)
    {
        _provider = provider;
        ViewModel = new CustomViewModel(
            async (p) => await SaveAsync(p as WidgetModel),
            async (p) => await ResetAsync()
        )
        {
            Model = Load()
        };
    }

    /// <summary>
    /// Load
    /// </summary>
    /// <returns>Widget Model</returns>
    public WidgetModel Load() =>
        _provider.Load<WidgetModel>(name) ?? GetDefaultWidget();

    /// <summary>
    /// View Model
    /// </summary>
    public CustomViewModel ViewModel { get; private set; }

    /// <summary>
    /// Updated Event
    /// </summary>
    public event EventHandler<WidgetEventArgs>? Updated;
}
