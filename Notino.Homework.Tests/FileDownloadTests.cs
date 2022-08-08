using FluentAssertions;
using Notino.Homework.Providers.TotalCommander;
using Notino.Homework.Requests;
using Xunit;

namespace Notino.Homework.Tests;

public class FileDownloadTests
{
    private readonly DownloadFileFromLocalStorage.Handler _sut;

    public FileDownloadTests()
    {
        var fileManager = new FileManager();
        _sut = new DownloadFileFromLocalStorage.Handler(fileManager);
    }

    [Fact]
    public async Task WhenRequestingDownloadFileFromLocalStorage_RequestedFileIsReturned()
    {
        CreateTestFile("_download.test");
        var filePath = Path.Combine(Environment.CurrentDirectory, "_download.test");
        var response = await _sut.Handle(new DownloadFileFromLocalStorage.Request()
        {
            Path = filePath
        }, CancellationToken.None);

        response.Name.Should().Be("_download.test");

        File.Delete(filePath);
    }

    private void CreateTestFile(string fileName)
    {
        File.Create(fileName).Dispose();
    }
}
