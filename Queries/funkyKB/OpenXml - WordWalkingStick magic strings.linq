<Query Kind="Statements" />

XNamespace pkg="http://schemas.microsoft.com/office/2006/xmlPackage";
XNamespace w = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";
var path = Util.CurrentQuery.GetLinqPadOpenXmlPath("WordWalkingStick - magic strings").Dump();
var xml = XDocument.Load(path);
var bodyPart = xml.Root
    .Elements(pkg + "part")
    .First(i => i.Attribute(pkg + "name").Value.Equals("/word/document.xml"))
    ;
//bodyPart.Dump();

var p = bodyPart
    .Element(pkg + "xmlData")
    .Element(w + "document")
    .Element(w + "body")
    .Element(w + "p")
    ;
//p.Dump();

var t = p
    .Element(w + "r")
    .Element(w + "t")
    ;
t.Dump();
