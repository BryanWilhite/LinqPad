<Query Kind="Program">
  <Namespace>System.Xml.Serialization</Namespace>
</Query>

void Main()
{
    var data = new OneTwoThree
    {
        One = "uno",
        Two = "dos",
        Three = "tres"
    };

    var elementTest = new Action<XDocument, string>((xd, elementName) =>
    {
        var test = (elementName == xd.Root.Element(elementName).Name);
        if(!test) throw new Exception("The expected element name is not here.");
    });

    var serializer = new XmlSerializer(typeof(OneTwoThree));
    MemoryStream stream = new MemoryStream();
    try
    {
        using (var writer = new XmlTextWriter(stream, Encoding.UTF8))
        {
            serializer.Serialize(stream, data);
            var xml = Encoding.UTF8.GetString(stream.ToArray());
            xml.Dump();

            var xd = XDocument.Parse(xml);
            var test = ("OneTwoThree" == xd.Root.Name);
            if (!test) throw new Exception("The expected element name is not here.");
            elementTest.Invoke(xd, "One");
            elementTest.Invoke(xd, "Two");
            elementTest.Invoke(xd, "Three");
        }
    }
    finally
    {
        if (stream != null) stream.Dispose();
    }
}

public class OneTwoThree
{
    public string One { get; set; }
    public string Two { get; set; }
    public string Three { get; set; }
}