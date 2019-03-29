<Query Kind="Statements" />

var s = "../../my/path/to/my.thing";

var matches = Regex.Matches(s, @"\.\./");

matches.Dump();