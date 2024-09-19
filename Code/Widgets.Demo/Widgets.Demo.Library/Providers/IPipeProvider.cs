namespace Widgets.Demo.Library.Providers;

/// <summary>
/// Pipe Provider
/// </summary>
public interface IPipeProvider
{
    /// <summary>
    /// Widget Server
    /// </summary>
    /// <param name="pipe">Pipe</param>
    void Server(string pipe);

    /// <summary>
    /// Client
    /// </summary>
    /// <param name="pipe">Pipe</param>
    /// <param name="state">State</param>
    void Client(string pipe, string content);

    /// <summary>
    /// Content Event
    /// </summary>
    event EventHandler<PipeContentEventArgs>? Content;
}
