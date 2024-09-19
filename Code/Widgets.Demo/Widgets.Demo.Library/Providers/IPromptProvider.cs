namespace Widgets.Demo.Library.Providers;

/// <summary>
/// Prompt Provider
/// </summary>
public interface IPromptProvider
{
    /// <summary>
    /// Get Data
    /// </summary>
    /// <param name="endpoint">Endpoint</param>
    /// <returns>Json Data</returns>
    string GetData(string endpoint);

    /// <summary>
    /// Load
    /// </summary>
    /// <returns>Prompt Model</returns>
    public PromptModel Load();

    /// <summary>
    /// View Model
    /// </summary>
    PromptViewModel ViewModel { get; }

    /// <summary>
    /// Updated Event
    /// </summary>
    event EventHandler<WidgetEventArgs>? Updated;

    /// <summary>
    /// Checked
    /// </summary>
    Func<DialogModel, Task<bool>> Checked { get; set; }
}
