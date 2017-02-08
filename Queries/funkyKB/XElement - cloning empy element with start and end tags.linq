<Query Kind="Program" />

// ref: http://blogs.msdn.com/b/ericwhite/archive/2009/07/08/empty-elements-and-self-closing-tags.aspx
void Main()
{
    XElement root = XElement.Parse("<Root></Root>");
    root.Dump("Original tree:");

    XElement rootClone = CloneElement(root);
    rootClone.Dump("Cloned tree:");
    var test1 = ("<Root />" == rootClone.ToString());
    if (!test1) throw new Exception("Default clone result not equal.");

    XElement rootCloneWithEmptyTags = CloneElement(root, renderEmptyTags: true);
    rootCloneWithEmptyTags.Dump("Cloned tree with empty tags:");
    var test2 = (root.ToString() == rootCloneWithEmptyTags.ToString());
    if (!test2) throw new Exception("Clone result with empty tags not equal.");
}

static XElement CloneElement(XElement element, bool renderEmptyTags = false)
{
    var isEmptyElement = new Func<XElement, bool>((xe) =>
    {
        return !xe.IsEmpty && !xe.Nodes().OfType<XText>().Any();
    });

    return new XElement(element.Name,
        element.Attributes(),
        element.Nodes().Select(n =>
        {
            XElement e = n as XElement;
            if (e != null) return CloneElement(e, renderEmptyTags);
            return n;
        }),
        (isEmptyElement.Invoke(element) && renderEmptyTags) ? string.Empty : null
    );
}