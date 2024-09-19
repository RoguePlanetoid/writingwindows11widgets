namespace Widgets.Demo.Library.Providers.Internal;

/// <summary>
/// Pipe Provider
/// </summary>
internal class PipeProvider : IPipeProvider
{
    private const string widget_server = ".";
    private const int server_instances = 1;

    /// <summary>
    /// Widget Server
    /// </summary>
    /// <param name="pipe">Pipe</param>
    public async void Server(string pipe)
    {
        while (true)
        {
            try
            {
                using var server = new NamedPipeServerStream(pipe,
                PipeDirection.InOut, server_instances,
                PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
                await server.WaitForConnectionAsync();
                using var reader = new StreamReader(server);
                var state = await reader.ReadLineAsync();
                Content?.Invoke(this, new PipeContentEventArgs(pipe, state));
            }
            catch { }
        }
    }

    /// <summary>
    /// Client
    /// </summary>
    /// <param name="pipe">Pipe</param>
    /// <param name="state">State</param>
    public async void Client(string pipe, string content)
    {
        try
        {
            using var client = new NamedPipeClientStream(widget_server, pipe,
            PipeDirection.InOut, PipeOptions.Asynchronous);
            await client.ConnectAsync();
            using var writer = new StreamWriter(client) { AutoFlush = true };
            writer.Write(content);
        }
        catch { }
    }

    /// <summary>
    /// Content Event
    /// </summary>
    public event EventHandler<PipeContentEventArgs>? Content;
}
