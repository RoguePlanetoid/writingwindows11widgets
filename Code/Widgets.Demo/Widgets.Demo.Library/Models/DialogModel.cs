namespace Widgets.Demo.Library.Models;

/// <summary>
/// Dialog Model
/// </summary>
public class DialogModel
{
    /// <summary>
    /// Is Valid
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Message
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Primary Option
    /// </summary>
    public string PrimaryOption { get; set; } = string.Empty;

    /// <summary>
    /// Secondary Option
    /// </summary>
    public string SecondaryOption { get; set; } = string.Empty;
}
