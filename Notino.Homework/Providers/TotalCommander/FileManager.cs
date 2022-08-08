using Notino.Homework.Extensions;

namespace Notino.Homework.Providers.TotalCommander;

public class FileManager : IFileManager
{
    private readonly ILogger<FileManager> _logger;
    private readonly HttpClient _httpClient;

    public FileManager(ILogger<FileManager> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public FileManager() { /* For Unit testing usage */ }

    public async Task SaveFileToLocalStorage(byte[] fileBytes, string path, string fileName, CancellationToken cancellationToken)
    {
        try
        {
            if (!Directory.Exists(path)) throw new DirectoryNotFoundException(path);

            var targetPath = Path.Combine(path, fileName);
            using FileStream fileStream = File.Open(targetPath, FileMode.Create, FileAccess.Write);
            await fileStream.WriteAsync(fileBytes, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new Exception(ex.Message);
        }
    }

    public async Task<byte[]> GetFileFromLocalStorage(string filePath, CancellationToken cancellationToken)
    {
        try
        {
            if (!File.Exists(filePath)) throw new FileNotFoundException(filePath);

            var fileBytes = await File.ReadAllBytesAsync(filePath, cancellationToken);

            return fileBytes;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new Exception(ex.Message);
        }
    }

    public async Task<byte[]> GetFileFromUrl(Uri url, CancellationToken cancellationToken)
    {
        try
        {
            using var response = await _httpClient.GetAsync(url, cancellationToken);

            return await response.HttpResponseToBytesAsync(url, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new Exception(ex.Message);
        }
    }
}
