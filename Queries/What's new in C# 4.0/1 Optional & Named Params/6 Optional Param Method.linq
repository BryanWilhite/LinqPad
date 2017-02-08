<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;System.Windows.Forms.dll</Reference>
  <Namespace>System.Security.AccessControl</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
</Query>

void Test (
	string p1 = "one",
	string p2 = "two",
	string p3 = "three",
	string p4 = "four")
{
	new { p1, p2, p3, p4 }.Dump();
}

void Main()
{
	Test();
}