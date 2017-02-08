<Query Kind="Program" />

void Write (IEnumerable<object> a)
{
	a.Dump();
}

void Main()
{
	Write (new List<string> { "foo" } );
	Write (new List<int>    { 12345 } );
}