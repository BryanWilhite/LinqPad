<Query Kind="Statements" />

var set = new Dictionary<string, string>
    {
        { "one", "This is one." },
        { "two", "This is two." },
        { "thirty-three", "This is 33." },
    };

var maxLength = set.Select(pair => pair.Key.Length).Max();
var padding = 4;

set
    .Select(pair =>
    {
        var count = (maxLength - pair.Key.Length) + padding;
        var spaces = Enumerable.Repeat(" ", count).ToArray();
        return $"{pair.Key}{string.Join(string.Empty, spaces)}{pair.Value}{Environment.NewLine}";
    })
    .Aggregate((a, i) => $"{a}{i}")
    .Dump();
