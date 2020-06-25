<Query Kind="Statements" />

char c = 's';
int i = c;
string hex = $"{i:X}";

c.Dump("char");
i.Dump("int");
hex.Dump("hex");

Convert.ToInt32(hex, 16).Dump("hex to int");
Char.ConvertFromUtf32(i).Dump("int to string");
Char.ConvertToUtf32(c.ToString(), 0).Dump("alternate char to int");
