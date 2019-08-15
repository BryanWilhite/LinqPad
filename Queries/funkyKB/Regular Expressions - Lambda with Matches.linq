<Query Kind="Statements" />

var text = @"
x [   ![Buy this DVD at Amazon.com!](http://kintespace.com/bitmaps/blog_darfur_diaries_dvd.jpg)](http://www.amazon.com/exec/obidos/ASIN/B000HCO8HC/thekintespacec00A/)Very [impressive](http://www.globalrights.org/site/PageServer?pagename=staff_bain)…
".Trim();

text.Dump("before Regex");

Regex.Replace(text, @"\[(\s+)\!\[[^[]+\]", m =>
{
    m.Groups.Dump("Match Groups");
    var group = m.Groups[1];
    m.Value.Dump("Match Value");
    return m.Value.Remove(1, group.Length);
}).Dump("after Regex");