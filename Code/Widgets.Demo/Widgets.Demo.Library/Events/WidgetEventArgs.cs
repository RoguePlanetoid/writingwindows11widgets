namespace Widgets.Demo.Library.Events;

/// <summary>
/// Widget Event Args
/// </summary>
/// <param name="model">Widget Model</param>
public class WidgetEventArgs(WidgetModel model) : EventArgs
{
    /// <summary>
    /// Widget Model
    /// </summary>
    public WidgetModel Model { get; set; } = model;
}
