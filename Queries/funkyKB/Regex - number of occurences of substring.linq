<Query Kind="Statements" />

var s1 = "../../my/path/to/my.thing";
var s2 = @"..\..\my\path\to\my.thing";

MatchCollection GetMatches(string s)
{
    var matches = Regex.Matches(s, @"\.\./|\.\.\\");
    return matches;
}

GetMatches(s1).Dump();
GetMatches(s2).Dump();
