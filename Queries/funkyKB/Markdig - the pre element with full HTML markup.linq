<Query Kind="Program">
  <NuGetReference>Markdig</NuGetReference>
  <Namespace>Markdig</Namespace>
</Query>

void Main()
{
    //https://dev.w3.org/html5/spec-preview/the-pre-element.html

    var md = @"# Markdown to `pre` containing Markdown
<pre>
    …to get _full_ HTML markup
    …to *not* preserve Markdown.

        …each line in this `pre` block
        has to be processed _individually_.
</pre>
    ".Trim();

    var raw = Markdown.ToHtml(md);
    raw.Dump("raw HTML from first markdig pass");

    var rawElement = XElement.Parse(string.Format("<raw>{0}</raw>", raw));
    var preElement = rawElement.Element("pre");
    MarkdownUtility.ConvertPreBlockToHtml(preElement);

    rawElement.ToString().Dump("raw HTML markup after processing");
}

public static class MarkdownUtility
{
    public static void ConvertPreBlockToHtml(XElement preElement)
    {
        var preList = GetPreLines(preElement);
        preElement.RemoveNodes();

        var fourSpaces = GetFourSpaces();
        var fourSpacesToken = GetFourSpacesToken();
        preList.ForEach(line =>
        {
            var p = Markdown.ToHtml(line.Replace(fourSpaces, fourSpacesToken));
            if (!IsMarkdownParagraph(p)) p = GetPElementWithNewLine();

            p = p.Replace(fourSpacesToken, fourSpaces);
            var pElement = XElement.Parse(p);
            pElement.Add(Environment.NewLine);
            preElement.Add(pElement.Nodes());
        });
    }

    public static string GetFourSpaces()
    {
        return new string(Enumerable.Repeat(' ', 4).ToArray());
    }

    public static string GetFourSpacesToken()
    {
        return "rx:four-spaces";
    }

    public static string GetPElementWithNewLine()
    {
        return string.Concat("<p>", Environment.NewLine, "</p>");
    }

    public static List<string> GetPreLines(XElement preElement)
    {
        /*
            Note: the first and last elements of preList
            *should* be empty (when no leading spaces before <pre />)
            or *should* contain `pre` open and closing, respectively
            (when there are leading spaces before <pre />).
        */
        var preList = preElement.Value
            .Split(Environment.NewLine.ToCharArray())
            .ToList();
        preList.RemoveAt(0);
        preList.RemoveAt(preList.Count() - 1);
        preList.Dump("<pre /> lines");

        return preList;
    }

    public static bool IsMarkdownParagraph(string p)
    {
        return p.StartsWith("<p>");
    }
}