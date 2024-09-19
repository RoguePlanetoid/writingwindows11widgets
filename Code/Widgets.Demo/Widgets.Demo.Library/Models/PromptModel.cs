namespace Widgets.Demo.Library.Models;

/// <summary>
/// Prompt Model
/// </summary>
public class PromptModel : WidgetModel
{
    private string _endpoint = string.Empty;
    private string _prompt = string.Empty;

    /// <summary>
    /// Endpoint
    /// </summary>
    public string Endpoint
    {
        get => _endpoint;
        set => SetProperty(ref _endpoint, value);
    }

    /// <summary>
    /// Prompt
    /// </summary>
    public string Prompt
    {
        get => _prompt;
        set => SetProperty(ref _prompt, value);
    }
}
