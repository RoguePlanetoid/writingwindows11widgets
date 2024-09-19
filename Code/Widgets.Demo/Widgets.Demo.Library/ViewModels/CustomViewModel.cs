namespace Widgets.Demo.Library.ViewModels;

/// <summary>
/// Custom View Model
/// </summary>
/// <param name="saveAction">Save Action</param>
/// <param name="resetAction">Reset Action</param>
public class CustomViewModel(
    Action<object?> saveAction, 
    Action<object?> resetAction) : ObservableBase
{
    private WidgetModel _model = new();

    /// <summary>
    /// Model
    /// </summary>
    public WidgetModel Model
    {
        get => _model;
        set => SetProperty(ref _model, value);
    }

    /// <summary>
    /// Save Action
    /// </summary>
    public ActionCommand SaveAction { get; } = new ActionCommand(saveAction);

    /// <summary>
    /// Reset Action
    /// </summary>
    public ActionCommand ResetAction { get; } = new ActionCommand(resetAction);
}
