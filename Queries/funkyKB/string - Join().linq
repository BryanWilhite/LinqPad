<Query Kind="Statements" />

var charArray = new char[] { 'A', 'B' };
var s = string.Join(string.Empty, charArray);
var test1 = ("AB" ==  s);
if(!test1) throw new Exception("The expected string is not here");

var input = "one two three four five";
var expected = "one two THREE four five";
var actual = string.Join(" ", input.Split(' ')
    .Select(i => (i == "three") ? i.ToUpper() : i).ToArray());
actual.Dump();
var test2 = (expected == actual);
if(!test2) throw new Exception("The expected and actual strings are not equal.");