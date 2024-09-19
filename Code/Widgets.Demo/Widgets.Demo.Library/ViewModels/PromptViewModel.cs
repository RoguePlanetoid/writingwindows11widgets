namespace Widgets.Demo.Library.ViewModels;

/// <summary>
/// Prompt View Model
/// </summary>
/// <param name="generateAction">Generate Action</param>
/// <param name="dataAction">Data Action</param>
/// <param name="saveAction">Save Action</param>
/// <param name="resetAction">Reset Action</param>
public class PromptViewModel(
    Action<object?> generateAction,
    Action<object?> dataAction, 
    Action<object?> saveAction, 
    Action<object?> resetAction) : ObservableBase
{
    private PromptModel _model = new();

    /// <summary>
    /// Model
    /// </summary>
    public PromptModel Model
    {
        get => _model;
        set => SetProperty(ref _model, value);
    }

    /// <summary>
    /// Generate Action
    /// </summary>
    public ActionCommand GenerateAction { get; } = new ActionCommand(generateAction);

    /// <summary>
    /// Data Action
    /// </summary>
    public ActionCommand DataAction { get; } = new ActionCommand(dataAction);

    /// <summary>
    /// Save Action
    /// </summary>
    public ActionCommand SaveAction { get; } = new ActionCommand(saveAction);

    /// <summary>
    /// Reset Action
    /// </summary>
    public ActionCommand ResetAction { get; } = new ActionCommand(resetAction);
}
