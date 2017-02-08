<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;System.Windows.Forms.dll</Reference>
  <Namespace>System.Security.AccessControl</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
</Query>

void Test (
	string p1 = "One",
	string p2 = "Two",
	string p3 = "Three",
	string p4 = "Four")
{
	new { p1, p2, p3, p4 }.Dump();
}

void Main()
{
	Test();	
	Test ("One", "Two", "Three", "Four");
	PowerPoint.ShowSlide (@"y:\docs\LINQPad 40 Queries\What's new in C# 4.0\OptionalParams.pptx");
}