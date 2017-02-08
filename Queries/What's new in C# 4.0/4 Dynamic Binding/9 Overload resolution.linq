<Query Kind="Program" />

void Main()
{
	object x = "string";
	Foo (x);
}

void Foo (int x) 	{ Console.WriteLine ("integer"); }
void Foo (string x)	{ Console.WriteLine ("string");  }
void Foo (object x)	{ Console.WriteLine ("object");  }

