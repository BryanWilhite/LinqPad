<Query Kind="Statements">
  <NuGetReference>HtmlAgilityPack</NuGetReference>
  <Namespace>HtmlAgilityPack</Namespace>
</Query>

//https://html-agility-pack.net/documentation
//https://devhints.io/xpath

var web = new HtmlWeb();

var htmlDoc = web.Load(@"https://codeopinion.com/avoiding-nullreferenceexception/");

//htmlDoc.DocumentNode.InnerHtml.Dump(nameof(htmlDoc.DocumentNode.InnerHtml));

var metaTwitterImage = htmlDoc.DocumentNode.SelectSingleNode("//meta[@name='twitter:image:src']");
metaTwitterImage?.OuterHtml.Dump(nameof(metaTwitterImage));

var metaTwitterHandle = htmlDoc.DocumentNode.SelectSingleNode("//meta[@name='twitter:site']");
metaTwitterHandle?.OuterHtml.Dump(nameof(metaTwitterHandle));

var metaTwitterTitle = htmlDoc.DocumentNode.SelectSingleNode("//meta[@name='twitter:title']");
metaTwitterTitle?.OuterHtml.Dump(nameof(metaTwitterTitle));

var metaTwitterDescription = htmlDoc.DocumentNode.SelectSingleNode("//meta[@name='twitter:description']");
metaTwitterDescription?.OuterHtml.Dump(nameof(metaTwitterDescription));

var metaOgImage = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:image']");
metaOgImage?.OuterHtml.Dump(nameof(metaOgImage));

var metaOgTitle = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:title']");
metaOgTitle.OuterHtml?.Dump(nameof(metaOgTitle));

var metaOgDescription = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:description']");
metaOgDescription?.OuterHtml.Dump(nameof(metaOgDescription));

var title = htmlDoc.DocumentNode.SelectSingleNode("//head/title");
title.InnerText.Dump(nameof(title.InnerText));