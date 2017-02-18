<Query Kind="Statements" />

var twoSpaces = "  ";
var two160s = "&#160;&#160;";

var xhtml = @"
    <div>
        <p>not indented</p>
        <p>    indented</p>
        <p>not indented</p>
        <p>    indented</p>
    </div>
".Trim();

var divElement = XElement.Parse(xhtml);
var pElements = divElement.Elements("p");

foreach (var p in pElements)
{
    p.Value = p.Value.Replace(twoSpaces, two160s);
}

divElement.ToString().Dump();