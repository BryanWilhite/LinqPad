<Query Kind="Program" />

void Main()
{
    var xhtml = "<p class=\"foo\">What I am seeing so far is Angular JS supporting one-way <code>checkbox</code> arrays—from the Model to the View via <code>ngRepeat</code>. There seems to be no support the other way around. So when we declare, <code>&lt;input name=\"checkSelections[]\" type=\"checkbox\" ng-model=\"foo\"&gt;</code>, Angular JS does not see the <code>checkSelection[]</code> array syntax and assumes <code>foo</code> is an array.</p><p>To further confuse ourselves, we could do something like this:</p><pre xml:space=\"preserve\">\n&lt;input name=\"checkSelections[]\" type=\"checkbox\" ng-model=\"foo[0]\"&gt;\n&lt;input name=\"checkSelections[]\" type=\"checkbox\" ng-model=\"foo[1]\"&gt;\n&lt;input name=\"checkSelections[]\" type=\"checkbox\" ng-model=\"foo[2]\"&gt;\n    </pre><p>Clicking on the second <code>checkbox</code>, would give us something almost useless: <code>foo = {\"1\" : true }</code>. To declare <code>ngTrueValue</code> or <code>ngFalseValue</code>, by the way, would cause an error. Any traditional <code>value=\"bar\"</code> declarations are ignored.</p><p>Apart from the confusion mentioned, it looks like Angular JS 1.x has no support for checkbox arrays (groups)—which has had server-side support for decades. It would not surprise me to find that none of the client-side frameworks has support for it out of the box.</p><h3>Related Links</h3><ul><li><code>ng-true-value</code> and <code>ng-false-value</code> for Angular check boxes [<a href=\"https://docs.angularjs.org/api/ng/input/input[checkbox]\">docs.angularjs.org</a>]\n        </li><li>AngularJS Form Validation with <code>ngMessages</code> <a href=\"https://scotch.io/tutorials/angularjs-form-validation-with-ngmessages\">[scotch.io]</a></li><li>Handling Checkboxes and Radio Buttons in Angular Forms <a href=\"https://scotch.io/tutorials/handling-checkboxes-and-radio-buttons-in-angular-forms\">[scotch.io]</a></li></ul>";
    if (!IsXml(xhtml))
    {
        "The expected XML/XHTML is not here.".Dump("Error");
        return;
    }

    var xDoc = XDocument.Parse(string.Format("<div>{0}</div>", xhtml).ToString());
    xDoc.Dump();

    var descendantsOfText = xDoc
        .Root
        .DescendantNodes()
        .Where(i => i.NodeType == XmlNodeType.Text);

    descendantsOfText.ToList()
        .ForEach(i => i.ToString().Dump(i.NodeType.ToString()));

    var displayText = string.Join(string.Empty, descendantsOfText.Select(i => i.ToString()).ToArray());
    displayText.Dump("Display Text");
}

static bool IsXml(string fragment)
{
    Match xmlMatch = Regex.Match(fragment, @"<([^>]+)>(.*?</(\1)>|[^>]*/>)");
    Match xmlMatchMinimized = Regex.Match(fragment, @"<([^>]+)/>");
    return (xmlMatch.Success || xmlMatchMinimized.Success);
}