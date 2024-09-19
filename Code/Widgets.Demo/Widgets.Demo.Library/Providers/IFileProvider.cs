namespace Widgets.Demo.Library.Providers;

/// <summary>
/// File Provider
/// </summary>
public interface IFileProvider
{
    /// <summary>
    /// Load
    /// </summary>
    /// <typeparam name="TFileData">File Data</typeparam>
    /// <param name="name">Filename</param>
    /// <returns>Loaded File Data</returns>
    TFileData? Load<TFileData>(string name) where TFileData : class;

    /// <summary>
    /// Load
    /// </summary>
    /// <typeparam name="TFileData">File Data</typeparam>
    /// <param name="name">Filename</param>
    /// <returns>Loaded File Data</returns>
    Task<TFileData?> LoadAsync<TFileData>(string name) where TFileData : class;

    /// <summary>
    /// Save
    /// </summary>
    /// <typeparam name="TFileData">File Data</typeparam>
    /// <param name="name">File Name</param>
    /// <param name="fileData">File Data</param>    
    /// <returns>True on Success, False if Not</returns>
    Task<bool> SaveAsync<TFileData>(string name, TFileData fileData) where TFileData : class;
}
