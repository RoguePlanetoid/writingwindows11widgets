namespace Widgets.Demo.Library.ViewModels;

/// <summary>
/// Counts View Model
/// </summary>
/// <param name="increment">Increment Action</param>
/// <param name="resetAction">Reset Action</param>
public class CountsViewModel(
    Action<object?> increment,
    Action<object?> resetAction) : ObservableBase
{
    private uint _value = 0;

    /// <summary>
    /// Value
    /// </summary>
    public uint Value
    {
        get => _value;
        set => SetProperty(ref _value, value);
    }

    /// <summary>
    /// Save Action
    /// </summary>
    public ActionCommand IncrementAction { get; } = new ActionCommand(increment);

    /// <summary>
    /// Reset Action
    /// </summary>
    public ActionCommand ResetAction { get; } = new ActionCommand(resetAction);
}
