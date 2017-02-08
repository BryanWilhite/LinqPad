<Query Kind="Statements" />

var xDoc = XDocument.Parse(@"
<root>
    <placeholder />
</root>
".Trim());

var editElement = XElement.Parse(@"
<edit>
    <p>One, <em>two</em>, three!</p>
    <p>That is the count.</p>
    <p>Nothing more than four!</p>
</edit>
".Trim());

xDoc.Root.Element("placeholder")
    .ReplaceWith(editElement.Elements());

xDoc.ToString().Dump();