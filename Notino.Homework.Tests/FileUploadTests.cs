using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Notino.Homework.Providers.TotalCommander;
using Notino.Homework.Requests;
using System.Text;
using Xunit;

namespace Notino.Homework.Tests;

public class FileUploadTests
{
    private readonly UploadFileToLocalStorage.Handler _sut;

    public FileUploadTests()
    {
        var fileManager = new FileManager();
        _sut = new UploadFileToLocalStorage.Handler(fileManager);
    }

    [Fact]
    public async Task GivenEmptyFile_WhenRequestingUploadToLocalStorage_FileIsSuccessfulyUploaded_And_CorrectResponseIsReturned()
    {
        var filePath = Path.Combine(Environment.CurrentDirectory, "_upload.test");
        var response = await _sut.Handle(new UploadFileToLocalStorage.Request()
        {
            File = GetFormFileFromString("", "_upload.test"),
            Path = Environment.CurrentDirectory
        }, CancellationToken.None);

        File.Exists(filePath).Should().BeTrue();
        filePath.Should().Be(Path.Combine(response.Path, response.Name));

        File.Delete(filePath);
    }

    public IFormFile GetFormFileFromString(string content, string fileName)
    {
        var bytes = Encoding.ASCII.GetBytes(content);

        return new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", fileName);
    }
}
