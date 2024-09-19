namespace Widgets.Demo.Desktop.Providers;

/// <summary>
/// File Provider
/// </summary>
internal class FileProvider : IFileProvider
{
    private readonly StorageFolder _folder = ApplicationData.Current.LocalFolder;

    /// <summary>
    /// Load
    /// </summary>
    /// <typeparam name="TFileData">File Data</typeparam>
    /// <param name="name">Filename</param>
    /// <returns>Loaded File Data</returns>
    public TFileData? Load<TFileData>(string name) where TFileData : class
    {
        try
        {         
            var storageFileTask = _folder.TryGetItemAsync(name).AsTask();
            storageFileTask.Wait();
            var storageFile = storageFileTask.Result;
            if (storageFile != null)
            {
                var readTextTask = FileIO.ReadTextAsync(storageFile as IStorageFile).AsTask();
                readTextTask.Wait();
                var fileData = readTextTask.Result;
                return JsonSerializer.Deserialize<TFileData>(fileData);
            }
            return default;
        }
        catch
        {
            return default;
        }
    }

    /// <summary>
    /// Load
    /// </summary>
    /// <typeparam name="TFileData">File Data</typeparam>
    /// <param name="name">Filename</param>
    /// <returns>Loaded File Data</returns>
    public async Task<TFileData?> LoadAsync<TFileData>(string name) where TFileData : class
    {
        try
        {
            var storageFile = await _folder.GetFileAsync(name);
            if (storageFile != null)
            {
                var content = await FileIO.ReadTextAsync(storageFile);
                return JsonSerializer.Deserialize<TFileData>(content);
            }
            return default;
        }
        catch
        {
            return default;
        }
    }

    /// <summary>
    /// Save
    /// </summary>
    /// <typeparam name="TFileData">File Data</typeparam>
    /// <param name="name">File Name</param>
    /// <param name="fileData">File Data</param>    
    /// <returns>True on Success, False if Not</returns>
    public async Task<bool> SaveAsync<TFileData>(string name, TFileData fileData) where TFileData : class
    {
        try
        {
            var content = JsonSerializer.Serialize(fileData);
            var file = await _folder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, content);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
