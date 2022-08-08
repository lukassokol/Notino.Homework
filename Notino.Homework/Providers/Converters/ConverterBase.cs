using Notino.Homework.Requests;

namespace Notino.Homework.Converters;

public abstract class ConverterBase
{
    public FileType SourceType { get; private set; }
    public FileType TargetType { get; private set; }

    public ConverterBase(FileType typeInput, FileType typeOutput)
    {
        SourceType = typeInput;
        TargetType = typeOutput;
    }

    public virtual string Convert(string doc) => throw new NotImplementedException();
}
