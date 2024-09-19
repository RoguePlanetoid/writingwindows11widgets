namespace Widgets.Demo.Library.Providers;

/// <summary>
/// Client Provider
/// </summary>
public interface IClientProvider
{
    /// <summary>
    /// Is Valid Endpoint
    /// </summary>
    /// <param name="url">Url</param>
    /// <returns>True if is, False if Not</returns>
    Task<bool> IsValidEndpointAsync(string url);

    /// <summary>
    /// Is Valid Json
    /// </summary>
    /// <param name="json">Json String</param>
    /// <returns>True if if, False if Not</returns>
    bool IsValidJson(string json);

    /// <summary>
    /// Get Data
    /// </summary>
    /// <param name="endpoint">Url</param>
    /// <returns>String</returns>
    Task<string> GetDataAsync(string endpoint);

    /// <summary>
    /// Get Data
    /// </summary>
    /// <param name="endpoint">Url</param>
    /// <returns>String</returns>
    string GetData(string endpoint);

    /// <summary>
    /// Get Card
    /// </summary>
    /// <param name="prompt">Prompt</param>
    /// <param name="data">Data</param>
    /// <returns>Adaptive Card</returns>
    Task<string> GetCardAsync(string prompt);
}
