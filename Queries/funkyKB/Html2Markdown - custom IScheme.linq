<Query Kind="Program">
  <NuGetReference>Html2Markdown</NuGetReference>
  <Namespace>Html2Markdown.Scheme</Namespace>
  <Namespace>Html2Markdown.Replacement</Namespace>
  <Namespace>HtmlAgilityPack</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
    var html = @"
<h1>Links</h1>
<div style=""text-align:center""><img alt=""Minimum Tags for XHTML Schema"" src=""http://kintespace.com/bitmaps/blog_word_2003_xhtml_tags.gif"" style=""text-align:center"" /></div>

<img alt=""My Son in Flight"" height=""384"" src=""http://kintespace.com/bitmaps/blog_son_in_flight.jpg"" style=""text-align:center"" width=""512"" />

<img alt=""The Sister with the Anime Eyes"" src=""http://kintespace.com/bitmaps/blog_sister_with_anime_eyes.jpg"" style=""float:right;"" />

<img alt=""Infinite Apocalypse Session"" height=""324"" src=""http://kintespace.com/bitmaps/blog_megafunk_lehua-sm_2509.jpg"" style=""float:right;padding:4px;"" width=""216"" />

<p style=""text-align:center""><img alt=""Lepl Humor"" src=""http://kintespace.com/bitmaps/blog_zonaeuropa_com.jpg"" /></p>
".Trim();

    var scheme = new MyMarkdownScheme();
    scheme.Replacers().Dump("Replacers");
    var converter = new Html2Markdown.Converter(scheme);
    var markdown = converter.Convert(html);
    markdown.Dump(nameof(markdown));
}

class MyMarkdownScheme : IScheme
{
    public IList<IReplacer> Replacers()
    {
        return _replacers;
    }

    private readonly IList<IReplacer> _replacers = new List<IReplacer>
    {
        new MyPatternReplacer
        {
            Pattern = @"</?(strong|b)>",
            Replacement = @"**"
        },
        new MyPatternReplacer
        {
            Pattern = @"</?(em|i)>",
            Replacement = @"*"
        },
        new MyPatternReplacer
        {
            Pattern = @"</h[1-6]>",
            Replacement = Environment.NewLine + Environment.NewLine
        },
        new MyPatternReplacer
        {
            Pattern = @"<h1[^>]*>",
            Replacement = Environment.NewLine + Environment.NewLine + "# "
        },
        new MyPatternReplacer
        {
            Pattern = @"<h2[^>]*>",
            Replacement = Environment.NewLine + Environment.NewLine + "## "
        },
        new MyPatternReplacer
        {
            Pattern = @"<h3[^>]*>",
            Replacement = Environment.NewLine + Environment.NewLine + "### "
        },
        new MyPatternReplacer
        {
            Pattern = @"<h4[^>]*>",
            Replacement = Environment.NewLine + Environment.NewLine + "#### "
        },
        new MyPatternReplacer
        {
            Pattern = @"<h5[^>]*>",
            Replacement = Environment.NewLine + Environment.NewLine + "##### "
        },
        new MyPatternReplacer
        {
            Pattern = @"<h6[^>]*>",
            Replacement = Environment.NewLine + Environment.NewLine + "###### "
        },
        new MyPatternReplacer
        {
            Pattern = @"<hr[^>]*>",
            Replacement = Environment.NewLine + Environment.NewLine + "* * *" + Environment.NewLine
        },
        new MyPatternReplacer
        {
            Pattern = @"<!DOCTYPE[^>]*>",
            Replacement = ""
        },
        new MyPatternReplacer
        {
            Pattern = @"</?html[^>]*>",
            Replacement = ""
        },
        new MyPatternReplacer
        {
            Pattern = @"</?head[^>]*>",
            Replacement = ""
        },
        new MyPatternReplacer
        {
            Pattern = @"</?body[^>]*>",
            Replacement = ""
        },
        new MyPatternReplacer
        {
            Pattern = @"<title[^>]*>.*?</title>",
            Replacement = ""
        },
        new MyPatternReplacer
        {
            Pattern = @"<meta[^>]*>",
            Replacement = ""
        },
        new MyPatternReplacer
        {
            Pattern = @"<link[^>]*>",
            Replacement = ""
        },
        new MyPatternReplacer
        {
            Pattern = @"<!--[^-]+-->",
            Replacement = ""
        },
        new MyPatternReplacer
        {
            Pattern = @"</?script[^>]*>",
            Replacement = ""
        },
        new MyCustomReplacer
        {
            CustomAction = HtmlParser.ReplaceImg
        },
        new MyCustomReplacer
        {
            CustomAction = HtmlParser.ReplaceLists
        },
        new MyCustomReplacer
        {
            CustomAction = HtmlParser.ReplaceAnchor
        },
        new MyCustomReplacer
        {
            CustomAction = HtmlParser.ReplaceCode
        },
        new MyCustomReplacer
        {
            CustomAction = HtmlParser.ReplacePre
        },
        new MyCustomReplacer
        {
            CustomAction = HtmlParser.ReplaceParagraph
        },
        new MyPatternReplacer
        {
            Pattern = @"<br[^>]*>",
            Replacement = @"  " + Environment.NewLine
        },
        new MyCustomReplacer
        {
            CustomAction = HtmlParser.ReplaceBlockquote
        },
        new MyCustomReplacer
        {
            CustomAction = HtmlParser.ReplaceEntites
        }
    };
}

// why is this internal? <https://github.com/baynezy/Html2Markdown/blob/master/src/Html2Markdown/Replacement/MyPatternReplacer.cs>
class MyPatternReplacer : IReplacer
{
    public string Pattern { get; set; }

    public string Replacement { get; set; }
    public string Replace(string html)
    {
        var regex = new Regex(Pattern);

        return regex.Replace(html, Replacement);
    }
}

// why is this internal? <https://github.com/baynezy/Html2Markdown/blob/master/src/Html2Markdown/Replacement/CustomReplacer.cs>
class MyCustomReplacer : IReplacer
{
    public string Replace(string html)
    {
        return CustomAction.Invoke(html);
    }

    public Func<string, string> CustomAction { get; set; }
}

// why is this internal? <https://github.com/baynezy/Html2Markdown/blob/master/src/Html2Markdown/Replacement/HtmlParser.cs>
static class HtmlParser
{
    private static readonly Regex NoChildren = new Regex(@"<(ul|ol)\b[^>]*>(?:(?!<ul|<ol)[\s\S])*?<\/\1>");

    internal static string ReplaceLists(string html)
    {
        var finalHtml = html;
        while (HasNoChildLists(finalHtml))
        {
            var listToReplace = NoChildren.Match(finalHtml).Value;
            var formattedList = ReplaceList(listToReplace);
            finalHtml = finalHtml.Replace(listToReplace, formattedList);
        }

        return finalHtml;
    }

    private static string ReplaceList(string html)
    {
        var list = Regex.Match(html, @"<(ul|ol)\b[^>]*>([\s\S]*?)<\/\1>");
        var listType = list.Groups[1].Value;
        var listItems = list.Groups[2].Value.Split(new[] { "</li>" }, StringSplitOptions.None);

        var counter = 0;
        var markdownList = new List<string>();
        listItems.ToList().ForEach(listItem =>
            {
                var listPrefix = (listType.Equals("ol")) ? string.Format("{0}.  ", ++counter) : "*   ";
                var finalList = Regex.Replace(listItem, @"<li[^>]*>", string.Empty);

                if (finalList.Trim().Length == 0)
                {
                    return;
                }

                finalList = Regex.Replace(finalList, @"^\s+", string.Empty);
                finalList = Regex.Replace(finalList, @"\n{2}", string.Format("{0}{1}    ", Environment.NewLine, Environment.NewLine));
                // indent nested lists
                finalList = Regex.Replace(finalList, @"\n([ ]*)+(\*|\d+\.)", string.Format("{0}$1    $2", "\n"));
                markdownList.Add(string.Format("{0}{1}", listPrefix, finalList));
            });

        return Environment.NewLine + Environment.NewLine + markdownList.Aggregate((current, item) => current + Environment.NewLine + item);
    }

    private static bool HasNoChildLists(string html)
    {
        return NoChildren.Match(html).Success;
    }

    internal static string ReplacePre(string html)
    {
        var doc = GetHtmlDocument(html);
        var nodes = doc.DocumentNode.SelectNodes("//pre");
        if (nodes == null)
        {
            return html;
        }

        nodes.ToList().ForEach(node =>
            {
                var tagContents = node.InnerHtml;
                var markdown = ConvertPre(tagContents);

                ReplaceNode(node, markdown);
            });

        return doc.DocumentNode.OuterHtml;
    }

    private static string ConvertPre(string html)
    {
        var tag = TabsToSpaces(html);
        tag = IndentNewLines(tag);
        return Environment.NewLine + Environment.NewLine + tag + Environment.NewLine;
    }

    private static string IndentNewLines(string tag)
    {
        return tag.Replace(Environment.NewLine, Environment.NewLine + "    ");
    }

    private static string TabsToSpaces(string tag)
    {
        return tag.Replace("\t", "    ");
    }

    internal static string ReplaceImg(string html)
    {
        var doc = GetHtmlDocument(html);
        var nodes = doc.DocumentNode.SelectNodes("//img");
        if (nodes == null)
        {
            return html;
        }

        nodes.ToList().ForEach(node =>
            {

                var src = node.Attributes.GetAttributeOrEmpty("src");
                var alt = node.Attributes.GetAttributeOrEmpty("alt");
                var title = node.Attributes.GetAttributeOrEmpty("title");

                var markdown = string.Format(@"![{0}]({1}{2})", alt, src, (title.Length > 0) ? string.Format(" \"{0}\"", title) : "");

                ReplaceNode(node, markdown);
            });

        return doc.DocumentNode.OuterHtml;
    }

    public static string ReplaceAnchor(string html)
    {
        var doc = GetHtmlDocument(html);
        var nodes = doc.DocumentNode.SelectNodes("//a");
        if (nodes == null)
        {
            return html;
        }

        nodes.ToList().ForEach(node =>
            {
                var linkText = node.InnerHtml;
                var href = node.Attributes.GetAttributeOrEmpty("href");
                var title = node.Attributes.GetAttributeOrEmpty("title");

                var markdown = "";

                if (!IsEmptyLink(linkText, href))
                {
                    markdown = string.Format(@"[{0}]({1}{2})", linkText, href,
                                             (title.Length > 0) ? string.Format(" \"{0}\"", title) : "");
                }

                ReplaceNode(node, markdown);
            });

        return doc.DocumentNode.OuterHtml;
    }

    public static string ReplaceCode(string html)
    {
        var finalHtml = html;
        var singleLineCodeBlocks = new Regex(@"<code>([^\n]*?)</code>").Matches(finalHtml);
        singleLineCodeBlocks.Cast<Match>().ToList().ForEach(block =>
        {
            var code = block.Value;
            var content = GetCodeContent(code);
            finalHtml = finalHtml.Replace(code, string.Format("`{0}`", content));
        });

        var multiLineCodeBlocks = new Regex(@"<code>([^>]*?)</code>").Matches(finalHtml);
        multiLineCodeBlocks.Cast<Match>().ToList().ForEach(block =>
        {
            var code = block.Value;
            var content = GetCodeContent(code);
            content = IndentLines(content).TrimEnd() + Environment.NewLine + Environment.NewLine;
            finalHtml = finalHtml.Replace(code, string.Format("{0}    {1}", Environment.NewLine, TabsToSpaces(content)));
        });

        return finalHtml;
    }

    public static string ReplaceBlockquote(string html)
    {
        var finalHtml = html;
        var doc = GetHtmlDocument(finalHtml);
        var nodes = doc.DocumentNode.SelectNodes("//blockquote");
        if (nodes == null)
        {
            return finalHtml;
        }

        nodes.ToList().ForEach(node =>
            {
                var quote = node.InnerHtml;
                var lines = quote.TrimStart().Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                var markdown = "";

                lines.ToList().ForEach(line =>
                    {
                        markdown += string.Format("> {0}{1}", line.TrimEnd(), Environment.NewLine);
                    });

                markdown = Regex.Replace(markdown, @"(>\s\r\n)+$", "");

                markdown = Environment.NewLine + Environment.NewLine + markdown + Environment.NewLine + Environment.NewLine;

                ReplaceNode(node, markdown);
            });

        return doc.DocumentNode.OuterHtml;
    }

    public static string ReplaceEntites(string html)
    {
        return WebUtility.HtmlDecode(html);
    }

    public static string ReplaceParagraph(string html)
    {
        var doc = GetHtmlDocument(html);
        var nodes = doc.DocumentNode.SelectNodes("//p");
        if (nodes == null)
        {
            return html;
        }

        nodes.ToList().ForEach(node =>
            {
                var text = node.InnerHtml;
                var markdown = Regex.Replace(text, @"\s+", " ");
                markdown = markdown.Replace(Environment.NewLine, " ");
                markdown = Environment.NewLine + Environment.NewLine + markdown + Environment.NewLine;
                ReplaceNode(node, markdown);
            });

        return doc.DocumentNode.OuterHtml;
    }

    private static bool IsEmptyLink(string linkText, string href)
    {
        var length = linkText.Length + href.Length;
        return length == 0;
    }

    private static HtmlDocument GetHtmlDocument(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        return doc;
    }

    private static void ReplaceNode(HtmlNode node, string markdown)
    {
        var markdownNode = HtmlNode.CreateNode(markdown);
        if (string.IsNullOrEmpty(markdown))
        {
            node.ParentNode.RemoveChild(node);
        }
        else
        {
            node.ParentNode.ReplaceChild(markdownNode.ParentNode, node);
        }

    }

    private static string IndentLines(string content)
    {
        var lines = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        return lines.Aggregate("", (current, line) => current + IndentLine(line));
    }

    private static string IndentLine(string line)
    {
        if (line.Trim().Equals(string.Empty))
        {
            return "";
        }
        return line + Environment.NewLine + "    ";
    }

    private static string GetCodeContent(string code)
    {
        var match = Regex.Match(code, @"<code[^>]*?>([^<]*?)</code>");
        var groups = match.Groups;
        return groups[1].Value;
    }
}

// <https://github.com/baynezy/Html2Markdown/blob/master/src/Html2Markdown/HtmlAgilityPackExtensions.cs>
static class HtmlAgilityPackExtensions
{
    public static string GetAttributeOrEmpty(this HtmlAttributeCollection collection, string attributeName)
    {
        return (collection[attributeName] == null) ? "" : collection[attributeName].Value;
    }
}
