namespace Widgets.Demo.Library.Providers;

/// <summary>
/// Counts Provider
/// </summary>
public interface ICountsProvider
{
    /// <summary>
    /// Set Initial State
    /// </summary>
    /// <param name="initialState">Initial State</param>
    void SetInitialState(Func<string, string?> initialState);

    /// <summary>
    /// View Model
    /// </summary>
    CountsViewModel ViewModel { get; }

    /// <summary>
    /// Updated Event
    /// </summary>
    event EventHandler<WidgetEventArgs>? Updated;
}
