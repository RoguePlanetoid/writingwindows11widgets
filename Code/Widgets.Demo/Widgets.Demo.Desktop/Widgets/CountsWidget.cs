namespace Widgets.Demo.Desktop.Widgets;

/// <summary>
/// Counts Widget
/// </summary>
internal class CountsWidget : WidgetBase
{
    private const string value = "count";
    private const string reset = "reset";
    private const string increment = "inc";
    private const string default_display = "0";
    private const string template = "ms-appx:///Widgets/Templates/CountsWidgetTemplate.json";
    private const string configure = "ms-appx:///Widgets/Templates/CountsWidgetConfigure.json";

    private readonly IPipeProvider? _provider;

    /// <summary>
    /// Definition Id
    /// </summary>
    public static string DefinitionId { get; } = nameof(CountsWidget);

    /// <summary>
    /// Configure
    /// </summary>
    protected bool Configure { get; set; }

    /// <summary>
    /// Value
    /// </summary>
    private uint Value { get; set; }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        state = Value.ToString();
        var updateOptions = new WidgetUpdateRequestOptions(Id)
        {
            Template = GetTemplateForWidget(),
            Data = GetDataForWidget(),
            CustomState = State
        };
        WidgetManager.GetDefault().UpdateWidget(updateOptions);
        _provider?.Client(DefinitionId, State);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="provider">Pipe Provider</param>
    /// <param name="widgetId">Widget Id</param>
    /// <param name="startingState">Starting State</param>
    public CountsWidget(IPipeProvider? provider, string widgetId, string startingState) : base(widgetId, startingState)
    {
        _provider = provider;
        if (state == string.Empty)
            state = default_display;
        else
            if (uint.TryParse(state, out uint parsedValue))
                Value = parsedValue;
    }

    /// <summary>
    /// On Action Invoked
    /// </summary>
    /// <param name="actionInvokedArgs">Action Invoked Args</param>
    public override void OnActionInvoked(WidgetActionInvokedArgs actionInvokedArgs)
    {
        if (actionInvokedArgs.Verb == increment)
        {
            if (uint.TryParse(State, out uint parsedValue))
                Value = parsedValue;
            Value++;
        }
        else if (actionInvokedArgs.Verb == reset)
        {
            Value = 0;
            Configure = false;
        }
        Update();
    }

    /// <summary>
    /// On Customization Requested
    /// </summary>
    /// <param name="customizationRequestedArgs">Customisation Requested Arguments</param>
    public override void OnCustomizationRequested(WidgetCustomizationRequestedArgs customizationRequestedArgs)
    {
        Configure = true;
        Update();
    }

    /// <summary>
    /// Activate
    /// </summary>
    public override void Activate() =>
        isActivated = true;

    /// <summary>
    /// Deactivate
    /// </summary>
    public override void Deactivate() =>
        isActivated = false;

    /// <summary>
    /// Get Template for Widget
    /// </summary>
    /// <returns>Widget Template</returns>
    public override string GetTemplateForWidget() => Configure ? 
        WidgetHelper.ReadJsonFromPackage(configure) : 
        WidgetHelper.ReadJsonFromPackage(template);

    /// <summary>
    /// Get Data for Widget
    /// </summary>
    /// <returns>Widget Data</returns>
    public override string GetDataForWidget()
    {
        var count = new JsonObject
        {
            [value] = State
        };
        return count.ToJsonString();
    }
}
