<Query Kind="Statements" />

var xml = @"
<root>
    <a>
        <b>
            <c>a text node</c>
        </b>
    </a>
</root>
";
var xd = XDocument.Parse(xml);

XNamespace empty = string.Empty;

var c = xd
    .Elements(empty + "root")
    .Elements(empty + "a")
    .Elements(empty + "b")
    .Elements(empty + "c");
if(c == null) throw new Exception("The expected `c` element is not here.");
c.Dump();

var test = (c.FirstOrDefault() == xd.Descendants(empty + "c").FirstOrDefault());
if(!test) throw new Exception("The expected `c` element child is not here.");