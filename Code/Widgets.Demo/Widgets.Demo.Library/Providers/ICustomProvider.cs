namespace Widgets.Demo.Library.Providers;

/// <summary>
/// Custom Provider
/// </summary>
public interface ICustomProvider
{
    /// <summary>
    /// Load
    /// </summary>
    /// <returns>Widget Model</returns>
    WidgetModel Load();

    /// <summary>
    /// View Model
    /// </summary>
    CustomViewModel ViewModel { get; }

    /// <summary>
    /// Updated Event
    /// </summary>
    event EventHandler<WidgetEventArgs>? Updated;
}
