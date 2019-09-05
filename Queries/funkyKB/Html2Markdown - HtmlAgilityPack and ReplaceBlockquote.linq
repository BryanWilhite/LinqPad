<Query Kind="Program">
  <NuGetReference>Html2Markdown</NuGetReference>
  <Namespace>HtmlAgilityPack</Namespace>
</Query>

void Main()
{
    var html = @"
<p><blockquote><p><strong>dA Poetry Lounge</strong> hosts an open mike spoken word on Tuesday nights, Greenway Court Theatre, 544 N. Fairfax near Melrose, LA, free parking next door at Fairfax High School, usually free, sometimes $2–$3, 9:00, sign up early by calling (213) 390-7072.</p></blockquote></p>

<blockquote><strong>TECHNOLOGY BREAKDOWN—Russell de Pina</strong>

<em>The New Builders</em>

Here we are at the end of another Black History Month, so before the contributions of African Americans are put on the back burner for another year, I’d like to give some shout-outs to folks that you probably never heard of. Their contributions have made possible much of the new media/digital revolution permeating our consciousness more and more each day.

<a href=""http://holyfamilyrockford.org/School/Classrooms/Smerko/Projects/Computer%20Engineers/Page14.htm"">Dr. Mark Hannah</a> not only was one of the cofounders of Silicon Graphics Inc., but <span style=""font-variant: small-caps"">he</span> holds the patents for the graphic engine technology that made Silicon Graphics the gold standard of computer graphics machines. SGI machines have been used for everything from molecular modeling to creating the special effects in “Terminator 2.” That’s right director James Cameron, you owe the success of “T2” to a brother!

J.W.Thompson created a language called “< a href = ""http://en.wikipedia.org/wiki/Lingo_programming_language"" > Lingo </ a >” for Macromedia.How significant is that you ask ? Well, every time you buy a multimedia CD - ROM title, the chances are that the people who authored that CD used “Lingo” to produce it.Every time you see a cool Flash movie on a website, you are looking at technology that originated from Mr.Thompson’s idea that multimedia could be scripted like a program.Despite the long reach of his work, magazines like < em > Wired </ em > have never acknowledged the contribution of this member of the black digerati.
       

       To quote Sun Microsystems, “the network IS the computer.” A chip produced by Advanced Micro Devices called the LANCE(Local Area Network Controller for Ethernet) was the power behind Sun’s network.Vern Coleman led the product development team at AMD that I worked on to create the LANCE chip in 1982. While other network chips came out during that time, the LANCE was designed into more systems than any competing designs.In the January issue of<em> Wired</em> magazine, there is an article about work being done at UC Berkeley on configurable interfaces for distributed computing elements.The LANCE sported a configurable interface design 18 years before<em>Wired</em> deemed the technology “new.”

We all know it was really the labor of African Americans that “built” the America of old.What you may not know is that we’re helping to build the world of the future as well.

<em>Russell de Pina invented the ABEL programming language for programmable logic design.ABEL made it possible for ASIC companies like Xilinx, Altera, Atmel and others to come into existence.Russell can be reached via email at rdepina@designgroup-studio.co.</em></blockquote>

<blockquote>Dear Mr. Wilhite:

Thank you for notifying us that you have posted a copy of bell hooks’ “Postmodern Blackness” to your website. Unfortunately, by doing so you have violated copyright law. According to PMC’s contract with its publisher, the author retains copyright of any work published in PMC, although PMC and Johns Hopkins University Press must be credited whenever an article is re-published. A third party can’t reprint or repost, without permissions from both author and, at this point, JHUP.

There is a relatively simple solution to this problem: rather than re-posting the article, you can include a link to the article as it originally appeared in Postmodern Culture. The link should be to PMC’s text only archive, which is accessible for free:

For an example of how such a link would work, please see .

If you choose to retain the currrent format for the re-posted article, you must secure permissions from both Professor hooks and Johns Hopkins University Press. I believe that you can contact Professor hooks via Oberlin College; the phone number for the English Department is (440)775-8570.

Please keep PMC updated on what course of action you choose to pursue.

Sincerely,

Lisa Spiro
Managing Editor
Postmodern Culture
(804)924-4527
fax (804) 982-2363
PMC@Jefferson.village.virginia.edu

PMC/IATH
Alderman Library, 3rd Floor
University of Virginia
Charlottesville, VA 22903</blockquote>
".Trim();

    ReplaceBlockquote(html).Dump();

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
        var markdown = string.Empty;
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
