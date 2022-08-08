using Newtonsoft.Json;
using Notino.Homework.Requests;

namespace Notino.Homework.Converters;

public class XmlToJsonConverter : ConverterBase
{
    public XmlToJsonConverter() : base(FileType.xml, FileType.json) { }

    public override string Convert(string doc)
    {
        var xml = new System.Xml.XmlDocument();
        xml.LoadXml(doc);

        var json = JsonConvert.SerializeXmlNode(xml, Formatting.Indented, true);

        return json;
    }
}
