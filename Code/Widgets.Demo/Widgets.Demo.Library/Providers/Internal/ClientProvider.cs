namespace Widgets.Demo.Library.Providers.Internal;

/// <summary>
/// Client Provider
/// </summary>
/// <param name="config">Client Config</param>
/// <param name="factory">Http Client Factory</param>
internal class ClientProvider(IClientConfig config, IHttpClientFactory factory) : IClientProvider
{
    private static readonly string empty = new JsonObject().ToJsonString();
    private readonly ChatGpt _chat = new(config.ApiKey);
    private readonly HttpClient _client = factory.CreateClient();

    /// <summary>
    /// Is Valid Endpoint
    /// </summary>
    /// <param name="url">Url</param>
    /// <returns>True if is, False if Not</returns>
    public async Task<bool> IsValidEndpointAsync(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return false;
        try
        {
            var uri = new Uri(url);
            var response = await _client.SendAsync(
                new HttpRequestMessage(HttpMethod.Head, uri));
            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Is Valid Json
    /// </summary>
    /// <param name="json">Json String</param>
    /// <returns>True if if, False if Not</returns>
    public bool IsValidJson(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return false;
        try
        {
            using var document = JsonDocument.Parse(json);
            return document != null;
        }
        catch (JsonException)
        {
            return false;
        }
    }

    /// <summary>
    /// Get Data
    /// </summary>
    /// <param name="endpoint">Url</param>
    /// <returns>String</returns>
    public async Task<string> GetDataAsync(string endpoint)
    {
        try
        {
            var response = await _client.GetStringAsync(endpoint);
            if (!IsValidJson(response))
                return empty;
            return response;
        }
        catch
        {
            return empty;
        }
    }

    /// <summary>
    /// Get Data
    /// </summary>
    /// <param name="endpoint">Url</param>
    /// <returns>String</returns>
    public string GetData(string endpoint)
    {
        try
        {
            var getStringTask = _client.GetStringAsync(endpoint);
            getStringTask.Wait();
            var response = getStringTask.Result;
            if (!IsValidJson(response))
                return empty;
            return response;
        }
        catch
        {
            return empty;
        }
    }

    /// <summary>
    /// Get Card
    /// </summary>
    /// <param name="prompt">Prompt</param>
    /// <param name="data">Data</param>
    /// <returns>Adaptive Card</returns>
    public async Task<string> GetCardAsync(string prompt)
    {
        var response = await _chat.Ask(prompt);
        if (!IsValidJson(response))
            return empty;
        return response;
    }
}