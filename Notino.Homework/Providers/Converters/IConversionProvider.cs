using Notino.Homework.Requests;

namespace Notino.Homework.Converters;

public interface IConversionProvider
{
    public ConverterBase GetConverter(FileType sourceType, FileType targetType);
}
