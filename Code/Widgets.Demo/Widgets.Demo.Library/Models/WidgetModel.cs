namespace Widgets.Demo.Library.Models;

/// <summary>
/// Widget Model
/// </summary>
public class WidgetModel : ObservableBase
{
    private static readonly string empty = new JsonObject().ToJsonString();

    private string _definitionId = empty;
    private string _template = empty;
    private string _data = empty;
    private string _state = empty;

    /// <summary>
    /// Definition Id
    /// </summary>
    public string DefinitionId
    {
        get => _definitionId;
        set => SetProperty(ref _definitionId, value);
    }

    /// <summary>
    /// Template
    /// </summary>
    public string Template
    {
        get => _template;
        set => SetProperty(ref _template, value);
    }

    /// <summary>
    /// Data
    /// </summary>
    public string Data
    {
        get => _data;
        set => SetProperty(ref _data, value);
    }

    /// <summary>
    /// State
    /// </summary>
    public string State
    {
        get => _state;
        set => SetProperty(ref _state, value);
    }
}
