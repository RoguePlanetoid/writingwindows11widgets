namespace Widgets.Demo.Library.Events;

/// <summary>
/// Pipe Content Event Args
/// </summary>
/// <param name="pipe">Pipe</param>
/// <param name="content">Content</param>
public class PipeContentEventArgs(string pipe, string? content) : EventArgs
{
    /// <summary>
    /// Pipe
    /// </summary>
    public string Pipe { get; set; } = pipe;

    /// <summary>
    /// Content
    /// </summary>
    public string? Content { get; set; } = content;
}
