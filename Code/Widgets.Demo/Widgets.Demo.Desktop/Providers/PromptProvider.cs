namespace Widgets.Demo.Desktop.Providers;

/// <summary>
/// Prompt Provider
/// </summary>
internal class PromptProvider : IPromptProvider
{
    private const string comma = ",";
    private const string close = "Close";
    private const string cancel = "Cancel";
    private const string confirm = "Confirm";
    private const string name = "prompt.json";
    private const string valid = "Widget Valid";
    private const string error = "Widget Error";
    private const string template = "ms-appx:///Widgets/Templates/PromptWidgetTemplate.json";
    private static readonly string defaultTemplate = WidgetHelper.ReadJsonFromPackage(template);

    private readonly IPromptConfig _config;
    private readonly IFileProvider _file;
    private readonly IClientProvider _client;

    /// <summary>
    /// Get Default Data
    /// </summary>
    /// <returns></returns>
    private static string GetDefaultData() => 
        new JsonObject().ToJsonString();

    /// <summary>
    /// Get Default Widget
    /// </summary>
    /// <returns>Prompt Model</returns>
    private static PromptModel GetDefaultWidget() => new()
    {
        DefinitionId = PromptWidget.DefinitionId,
        Template = defaultTemplate,
        Data = GetDefaultData()
    };

    /// <summary>
    /// Is Valid Card
    /// </summary>
    /// <param name="json">Json</param>
    /// <param name="message">Validation Message</param>
    /// <returns>True if is, False if Not</returns>
    private static bool IsValidCard(string json, out string message)
    {
        try
        {
            message = string.Join(comma, 
                AdaptiveCard.FromJson(json)
                .Warnings.Select(s => s.Message));
            return true;
        }
        catch(AdaptiveSerializationException ex)
        {
            message = ex.Message;
            return false;
        }
    }

    /// <summary>
    /// Generate
    /// </summary>
    /// <param name="model">Model</param>
    private async Task GenerateAsync(PromptModel? model)
    {
        if (model != null)
        {            
            var prompt = model.Prompt;
            var data = GetDefaultData();
            if (await _client.IsValidEndpointAsync(model.Endpoint))
            {
                data = await _client.GetDataAsync(model.Endpoint);
                if (!_client.IsValidJson(data))
                    data = GetDefaultData();
                prompt = string.Format(_config.JsonPrompt, model.Endpoint, data);
            }
            if (!string.IsNullOrWhiteSpace(prompt))
            {
                var template = await _client.GetCardAsync(prompt);
                if (_client.IsValidJson(template))
                {
                    model.State = model.Endpoint;
                    model.Template = template;
                    model.Prompt = prompt;
                    model.Data = data;
                }
            }
        }
    }

    /// <summary>
    /// Data
    /// </summary>
    /// <param name="model">Prompt Model</param>    
    private async Task DataAsync(PromptModel? model)
    {
        if (model != null && await _client.IsValidEndpointAsync(model.Endpoint))
        {
            var data = await _client.GetDataAsync(model.Endpoint);
            if (!_client.IsValidJson(data))
                data = GetDefaultData();
            model.Data = data;
        }
    }

    /// <summary>
    /// Save Widget
    /// </summary>
    /// <param name="model">Prompt Model</param>
    /// <returns>True if Saved, False if Not</returns>
    private async Task<bool> SaveWidget(PromptModel? model)
    {
        if (model == null)
            return false;
        var success = await _file.SaveAsync(name, model);
        if (success)
            Updated?.Invoke(this, new WidgetEventArgs(model));
        return success;
    }

    /// <summary>
    /// Save
    /// </summary>
    /// <param name="model">Prompt Model</param>
    /// <returns>True on Success, False if Not</returns>
    private async Task SaveAsync(PromptModel? model)
    {
        if (model == null)
            return;
        var isValid = IsValidCard(model.Template, out var message);
        var dialog = new DialogModel()
        {
            IsValid = isValid,
            Message = message,
            Title = isValid ? valid : error,
            PrimaryOption = isValid ? confirm : close,
            SecondaryOption = isValid ? cancel : string.Empty
        };
        var result = await Checked(dialog);
        if (result)
            await SaveWidget(model);
    }

    /// <summary>
    /// Reset
    /// </summary>
    private async Task ResetAsync()
    {
        if (await SaveWidget(GetDefaultWidget()))
            ViewModel.Model = GetDefaultWidget();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="config">Prompt Config</param>
    /// <param name="file">File Provider</param>
    /// <param name="client">Client Provider</param>
    public PromptProvider(IPromptConfig config, IFileProvider file, IClientProvider client)
    {
        _config = config;
        _file = file;
        _client = client;
        ViewModel = new PromptViewModel(       
            async (p) => await GenerateAsync(p as PromptModel),
            async (p) => await DataAsync(p as PromptModel),
            async (p) => await SaveAsync(p as PromptModel),
            async (p) => await ResetAsync()
        )
        {
            Model = Load()
        };
    }

    /// <summary>
    /// Get Data
    /// </summary>
    /// <param name="endpoint">Endpoint</param>
    /// <returns>Json Data</returns>
    public string GetData(string endpoint)
    {
        var data = _client.GetData(endpoint);
        if (!_client.IsValidJson(data))
            data = GetDefaultData();
        return data;
    }

    /// <summary>
    /// Load
    /// </summary>
    /// <returns>Prompt Model</returns>
    public PromptModel Load() =>
        _file.Load<PromptModel>(name) ?? GetDefaultWidget();

    /// <summary>
    /// View Model
    /// </summary>
    public PromptViewModel ViewModel { get; private set; }

    /// <summary>
    /// Updated Event
    /// </summary>
    public event EventHandler<WidgetEventArgs>? Updated;

    /// <summary>
    /// Checked
    /// </summary>
    public Func<DialogModel,Task<bool>> Checked { get; set; } = (check) => 
        Task.FromResult(false);
}
