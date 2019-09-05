<Query Kind="Statements">
  <NuGetReference>Html2Markdown</NuGetReference>
</Query>

var html = @"
<h1>Links</h1>
<div style=""text-align:center""><img alt=""Minimum Tags for XHTML Schema"" src=""http://kintespace.com/bitmaps/blog_word_2003_xhtml_tags.gif"" style=""text-align:center"" /></div>

<img alt=""My Son in Flight"" height=""384"" src=""http://kintespace.com/bitmaps/blog_son_in_flight.jpg"" style=""text-align:center"" width=""512"" />

<img alt=""The Sister with the Anime Eyes"" src=""http://kintespace.com/bitmaps/blog_sister_with_anime_eyes.jpg"" style=""float:right;"" />

<img alt=""Infinite Apocalypse Session"" height=""324"" src=""http://kintespace.com/bitmaps/blog_megafunk_lehua-sm_2509.jpg"" style=""float:right;padding:4px;"" width=""216"" />

<p style=""text-align:center""><img alt=""Lepl Humor"" src=""http://kintespace.com/bitmaps/blog_zonaeuropa_com.jpg"" /></p>
".Trim();

var scheme = new Html2Markdown.Scheme.Markdown();
scheme.Replacers().Dump("Replacers");
var converter = new Html2Markdown.Converter(scheme);
var markdown = converter.Convert(html);
markdown.Dump(nameof(markdown));