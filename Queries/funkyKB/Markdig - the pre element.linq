<Query Kind="Statements">
  <NuGetReference>Markdig</NuGetReference>
  <Namespace>Markdig</Namespace>
</Query>

//https://dev.w3.org/html5/spec-preview/the-pre-element.html

var md = @"# Markdown to `pre`
````c#
    //this _emphasis_ should be preserved.
    var x = 10;
````
".Trim();
Markdown.ToHtml(md).Dump("<pre /> with <code />");

md = @"# Markdown inferring a `pre`

This should be a `p` tag.

    Markdig should “infer” a `pre` here.
        …continuing here because of four-space indentation.

This should also be a `p` tag.

".Trim();
Markdown.ToHtml(md).Dump("<pre /> from four-space indentation");

md = @"# Markdown to `pre` containing Markdown
<pre>

…this Markdown _emphasis_

should be *preserved* (with `p`-ish linebreaks).

    …because it is wrapped

    in a `pre`.

</pre>
<div>

…this Markdown _emphasis_

is *not preserved* (with `p`-ish linebreaks).

…because it is *not* wrapped

in a `pre` (and not indented).

</div>
".Trim();
Markdown.ToHtml(md).Dump("<pre /> preserving Markdown");
