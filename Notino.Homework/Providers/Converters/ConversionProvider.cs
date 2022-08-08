using Notino.Homework.Requests;

namespace Notino.Homework.Converters;

public class ConversionProvider : IConversionProvider
{
    private readonly List<ConverterBase> _converters;

    public ConversionProvider()
    {
        _converters = new List<ConverterBase>()
        {
            new XmlToJsonConverter(),
            new JsonToXmlConverter()
        };
    }

    public ConverterBase GetConverter(FileType sourceType, FileType targetType)
    {
        return _converters.First(x => x.SourceType == sourceType && x.TargetType == targetType);
    }
}
