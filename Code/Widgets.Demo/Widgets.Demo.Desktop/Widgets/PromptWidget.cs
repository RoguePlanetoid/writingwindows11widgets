namespace Widgets.Demo.Desktop.Widgets;

/// <summary>
/// Prompt Widget
/// </summary>
/// <param name="provider">Prompt Provider</param>
/// <param name="widgetId">Widget Id</param>
/// <param name="startingState">Starting State</param>
internal class PromptWidget(IPromptProvider? provider, string widgetId, string startingState) : 
    WidgetBase(widgetId, startingState)
{
    /// <summary>
    /// Definition Id
    /// </summary>
    public static string DefinitionId { get; } = nameof(PromptWidget);

    /// <summary>
    /// On Action Invoked
    /// </summary>
    /// <param name="actionInvokedArgs"Action Invoked Args></param>
    public override void OnActionInvoked(WidgetActionInvokedArgs actionInvokedArgs)
    {
        var endpoint = State;
        var updateOptions = new WidgetUpdateRequestOptions(Id)
        {
            Data = provider!.GetData(endpoint),
            CustomState = State
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
