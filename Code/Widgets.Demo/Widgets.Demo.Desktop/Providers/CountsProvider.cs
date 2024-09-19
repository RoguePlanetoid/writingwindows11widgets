namespace Widgets.Demo.Desktop.Providers;

/// <summary>
/// Counts Provider
/// </summary>
internal class CountsProvider : ICountsProvider
{
    /// <summary>
    /// Set
    /// </summary>
    /// <param name="value">Value</param>
    private void Set(uint value)
    {
        var widget = new WidgetModel()
        {
            DefinitionId = CountsWidget.DefinitionId,
            State = value.ToString()
        };
        Updated?.Invoke(this, new WidgetEventArgs(widget));
    }

    /// <summary>
    /// Increment
    /// </summary>
    private void Increment()
    {
        ViewModel.Value++;
        Set(ViewModel.Value);
    }

    /// <summary>
    /// Reset
    /// </summary>
    private void Reset()
    {
        ViewModel.Value = 0;
        Set(ViewModel.Value);
    }

    /// <summary>
    /// Set Latest State
    /// </summary>
    /// <param name="args">Event Args</param>
    private void SetLatestState(PipeContentEventArgs args)
    {
        if (args.Pipe == CountsWidget.DefinitionId &&
            uint.TryParse(args.Content, out var value))
                ViewModel.Value = value;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="provider">Pipe Provider</param>
    public CountsProvider(IPipeProvider provider)
    {
        ViewModel = new CountsViewModel(
            (p) => Increment(),
            (p) => Reset()
        );
        provider.Content += (object? sender, PipeContentEventArgs e) => 
            SetLatestState(e);
    }

    /// <summary>
    /// Set Initial State
    /// </summary>
    /// <param name="initialState">Initial State</param>
    public void SetInitialState(Func<string, string?> initialState)
    {
        if (uint.TryParse(initialState(CountsWidget.DefinitionId), out var value))
            ViewModel.Value = value;
    }

    /// <summary>
    /// View Model
    /// </summary>
    public CountsViewModel ViewModel { get; private set; }

    /// <summary>
    /// Updated Event
    /// </summary>
    public event EventHandler<WidgetEventArgs>? Updated;
}
