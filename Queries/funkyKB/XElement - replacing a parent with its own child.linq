<Query Kind="Statements" />

//read Jon Skeet: http://stackoverflow.com/a/42269519/22944

var xml = @"
<div>
<p>
    <h2>hey</h2>
</p>
<pre />
<h2 class=""cool"" />
<p>
    <h2>okay</h2>
</p>
</div>
".Trim();

var div = XElement.Parse(xml);
var h2Elements = div.Descendants("h2");
h2Elements.ToList().ForEach(i =>
{
    if(i.Parent.Name != "p") return;
    i.Parent.ReplaceWith(i);
});

div.Dump();