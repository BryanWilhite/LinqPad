<Query Kind="Program" />

void Main()
{
    var html = @"<a href=""https://wpfbiggestbox.codeplex.com"" width=""100%"" title=""Songhay BiggestBox on the Desktop - Basic Validation Sample"" attrOne style=""display:block;margin:16px;margin-left:auto;margin-right:auto"" attrTwo>";

    Regex re;
    MatchEvaluator me;

    //Find HTML5 attributes:
    re = new Regex(@"<[^>]+>", RegexOptions.IgnoreCase);
    me = new MatchEvaluator(EvaluateElementForMinimizedAttribute);
    re.Replace(html, me).Dump();
}

static string EvaluateElementForMinimizedAttribute(Match match)
{
    var s = match.Value;

    var placeholderPrefix = "!*m";
    var placeholderTemplate = string.Concat(placeholderPrefix, "{0}");

    //remove strings between quotes:
    var betweenQuotes = Regex.Matches(s, @"([""'])(?:(?=(\\?))\2.)*?\1", RegexOptions.IgnoreCase);
    foreach (Match m in betweenQuotes)
    {
        var placeholder = string.Format(placeholderTemplate, m.Index);
        s = s.Replace(m.Value, string.Format(placeholderTemplate, m.Index));
    }

    //evaluate what was not removed:
    var possibilities = Regex.Matches(s, @"(\b[^\s]+\b)", RegexOptions.IgnoreCase);
    foreach (Match m in possibilities)
    {
        if(m.Index == 1) continue; //match should not be element name
        if (m.Value.Contains("=")) continue; //match should not be attribute-value pair
        s = s.Replace(m.Value, string.Format(@"{0}=""{0}""", m.Value));
    }

    //restore strings between quotes:
    foreach (Match m in betweenQuotes)
    {
        var reArg = string.Concat(Regex.Escape(placeholderPrefix), m.Index, @"\b");
        var re = new Regex(reArg);
        s = re.Replace(s, m.Value, 1);
    }

    return s;
}
