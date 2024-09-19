namespace Widgets.Demo.Desktop.Widgets;

/// <summary>
/// Custom Widget
/// </summary>
/// <param name="provider">Custom Provider</param>
/// <param name="widgetId">Widget Id</param>
/// <param name="startingState">Starting State</param>
internal class CustomWidget(ICustomProvider? provider, string widgetId, string startingState) : 
    WidgetBase(widgetId, startingState)
{
    /// <summary>
    /// Definition Id
    /// </summary>
    public static string DefinitionId { get; } = nameof(CustomWidget);

    /// <summary>
    /// On Action Invoked
    /// </summary>
    /// <param name="actionInvokedArgs"Action Invoked Args></param>
    public override void OnActionInvoked(WidgetActionInvokedArgs actionInvokedArgs)
    {
        var updateOptions = new WidgetUpdateRequestOptions(Id)
        {
            Data = GetDataForWidget(),
            CustomState = actionInvokedArgs.CustomState
        };
        WidgetManager.GetDefault().UpdateWidget(updateOptions);
    }

    /// <summary>
    /// Get Template for Widget
    /// </summary>
    /// <returns>Widget Template</returns>
    public override string GetTemplateForWidget() =>
        provider!.Load().Template;

    /// <summary>
    /// Get Data for Widget
    /// </summary>
    /// <returns>Widget Data</returns>
    public override string GetDataForWidget() =>
        provider!.Load().Data;
}
