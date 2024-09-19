namespace Widgets.Demo.Desktop.Pages;

/// <summary>
/// Prompt Page
/// </summary>
public sealed partial class PromptPage : Page
{
    private readonly IPromptProvider? _provider;
    private Dialog? _dialog;

    /// <summary>
    /// Check
    /// </summary>
    /// <param name="model">Widget Model</param>
    private async Task<bool> Checked(DialogModel model)
    {
        _dialog ??= new Dialog(XamlRoot);
        if(model.IsValid)
            return await _dialog.ConfirmAsync(
                model.Message,
                model.PrimaryOption, 
                model.SecondaryOption, 
                model.Title);
        else
        {
            await _dialog.ShowAsync(
                model.Message, 
                model.PrimaryOption, 
                model.Title);
            return false;
        }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public PromptPage()
    {
        InitializeComponent();
        _provider = App.Host?.Services.GetService<IPromptProvider>();
        Display.DataContext = _provider!.ViewModel;
        _provider!.Updated += (object? sender, WidgetEventArgs e) =>
            App.SetWidgetState(e.Model);
        _provider!.Checked += Checked;
    }
}
