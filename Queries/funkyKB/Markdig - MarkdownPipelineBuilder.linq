<Query Kind="Statements">
  <NuGetReference>Markdig</NuGetReference>
  <Namespace>Markdig</Namespace>
</Query>

MarkdownPipeline pipline;
string html;
string markdown;

markdown = "# Header 1";

pipline = new MarkdownPipelineBuilder()
  .Build();

html = Markdown.ToHtml(markdown, pipline); // <h1>Header 1</h1>
html.Dump();

pipline = new MarkdownPipelineBuilder()
  .UseAutoIdentifiers() // enable the Auto Identifiers extension
  .Build();

html = Markdown.ToHtml(markdown, pipline); // <h1 id="header-1">Header 1</h1>
html.Dump();
