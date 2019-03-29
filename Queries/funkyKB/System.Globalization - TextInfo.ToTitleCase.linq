<Query Kind="Statements">
  <Namespace>System.Globalization</Namespace>
</Query>

var textInfo = new CultureInfo(CultureInfo.CurrentCulture.LCID).TextInfo;
textInfo.ToTitleCase("hello world").Dump();