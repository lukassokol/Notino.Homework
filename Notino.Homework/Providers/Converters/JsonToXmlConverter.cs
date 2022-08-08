using Newtonsoft.Json;
using Notino.Homework.Requests;

namespace Notino.Homework.Converters;

public class JsonToXmlConverter : ConverterBase
{
    public JsonToXmlConverter() : base(FileType.json, FileType.xml) { }

    public override string Convert(string doc)
    {
        var node = JsonConvert.DeserializeXNode(doc, "Root");

        if (node == null)
            return string.Empty;

        return node.ToString();
    }
}