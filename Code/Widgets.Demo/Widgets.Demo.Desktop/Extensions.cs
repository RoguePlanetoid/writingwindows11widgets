namespace Widgets.Demo.Desktop;

/// <summary>
/// Extensions
/// </summary>
internal static class Extensions
{
    private const string app_settings = "appsettings.json";
    private const string client_settings = "appsettings.client.json";

    /// <summary>
    /// Add Config Services
    /// </summary>
    /// <param name="services">Service Collection</param>
    /// <param name="root">Configuration Root</param>
    /// <returns>Service Collection</returns>
    private static IServiceCollection AddConfigServices(this IServiceCollection services, IConfigurationRoot root) =>
        services.AddSingleton<IClientConfig>(root.GetSection(nameof(ClientConfig)).Get<ClientConfig>() ?? new())
        .AddSingleton<IPromptConfig>(root.GetSection(nameof(PromptConfig)).Get<PromptConfig>() ?? new());

    /// <summary>
    /// Add Config
    /// </summary>
    /// <param name="services">Service Collection</param>
    /// <returns>Service Collection</returns>
    private static IServiceCollection AddConfig(this IServiceCollection services) =>
        services.AddConfigServices(new ConfigurationBuilder()
        .AddJsonFile(client_settings, true, true)
        .AddJsonFile(app_settings, true, true)
        .Build());

    /// <summary>
    /// Add Services
    /// </summary>
    /// <param name="services">Service Collection</param>
    /// <returns>Service Collection</returns>
    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services.AddLibrary()
        .AddSingleton<IFileProvider, FileProvider>()
        .AddSingleton<ICountsProvider, CountsProvider>()
        .AddSingleton<ICustomProvider, CustomProvider>()
        .AddSingleton<IPromptProvider, PromptProvider>()
        .AddTransient<MainWindow>()
        .AddConfig();
}
