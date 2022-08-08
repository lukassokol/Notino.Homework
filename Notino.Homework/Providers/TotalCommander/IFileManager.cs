namespace Notino.Homework.Providers.TotalCommander;

public interface IFileManager
{
    public Task SaveFileToLocalStorage(byte[] fileBytes, string targetPath, string fileName, CancellationToken cancellationToken);
    public Task<byte[]> GetFileFromLocalStorage(string filePath, CancellationToken cancellationToken);
    public Task<byte[]> GetFileFromUrl(Uri url, CancellationToken cancellationToken);

}
