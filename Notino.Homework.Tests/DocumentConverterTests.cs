using FluentAssertions;
using FluentAssertions.Json;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Notino.Homework.Converters;
using Notino.Homework.Requests;
using System.Text;
using System.Xml.Linq;
using Xunit;

namespace Notino.Homework.Tests;

public class DocumentConverterTests
{
    private const string json =
            @"{
              'name': 'Lukas Sokol',
              'age': 23
            }";
    private const string xml =
            @"<Person>
                <Name>Lukas Sokol</Name>
                <Age>23</Age>
            </Person>";

    private readonly DocumentConverter.Handler _sut;

    public DocumentConverterTests()
    {
        //_conversionProvider = Substitute.For<IConversionProvider>();
        var conversionProvider = new ConversionProvider();
        _sut = new DocumentConverter.Handler(conversionProvider);
    }

    [Fact]
    public async Task GivenJsonDoc_WhenRequestingConvertToXml_ValidDocIsReturned()
    {
        var response = await _sut.Handle(new DocumentConverter.Request()
        {
            OutputType = FileType.xml,
            SourceFile = GetFormFileFromString(json, "data.json")
        }, CancellationToken.None); ;
        var xml = XDocument.Parse(Encoding.ASCII.GetString(response.Bytes)).Root;

        xml.Element("name").Value.Should().BeEquivalentTo("Lukas Sokol");
        xml.Element("age").Value.Should().BeEquivalentTo("23");
        response.Name.Should().BeEquivalentTo("data.xml");
    }

    [Fact]
    public async Task GivenXmlDoc_WhenRequestingConvertToJson_ValidDocIsReturned()
    {
        var jsonShouldBe = JToken.FromObject(new { Name = "Lukas Sokol", Age = "23" });

        var response = await _sut.Handle(new DocumentConverter.Request()
        {
            OutputType = FileType.json,
            SourceFile = GetFormFileFromString(xml, "data.xml")
        }, CancellationToken.None);
        var jsonResponse = JToken.Parse(Encoding.ASCII.GetString(response.Bytes));

        jsonShouldBe.Should().BeEquivalentTo(jsonResponse);
        response.Name.Should().BeEquivalentTo("data.json");
    }

    [Fact]
    public async Task GivenXmlDoc_WhenRequestingConvertingToXml_InvalidOperationExceptionIsThrown()
    {
        try
        {
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _sut.Handle(new DocumentConverter.Request()
            {
                OutputType = FileType.xml,
                SourceFile = GetFormFileFromString(xml, "data.xml")
            }, CancellationToken.None));
        }
        catch (Exception ex)
        {
        }
    }

    public IFormFile GetFormFileFromString(string content, string fileName)
    {
        var bytes = Encoding.ASCII.GetBytes(content);

        return new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", fileName);
    }
}
