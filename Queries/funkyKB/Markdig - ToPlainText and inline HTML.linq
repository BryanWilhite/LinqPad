<Query Kind="Statements">
  <NuGetReference>Markdig</NuGetReference>
  <Namespace>Markdig</Namespace>
</Query>

//inline HTML is passed through by default

var md = @"# “Why I stopped using NGRX” and other tweeted links…

<div class=""tweet"" data-status-id=""1130914847938502700"">

[<img alt=""Denis Levkov [DenisLevkov9]"" src=""https://songhay.blob.core.windows.net:443/shared-social-twitter/DenisLevkov9.jpg"" />](https://twitter.com/DenisLevkov9)
Why I stopped using NGRX by the great wizard of none [https://link.medium.com/AS542V3uSW](https://link.medium.com/AS542V3uSW)

</div>
".Trim();

Markdown.ToPlainText(md).Dump($"{nameof(Markdown.ToPlainText)}");

Markdown.ToHtml(md).Dump($"{nameof(Markdown.ToHtml)}");
