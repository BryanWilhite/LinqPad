<Query Kind="Program" />

void Main()
{
    /*
        The URI equivalent of System.IO.Path.Combine()
        is this UriBuilder pattern:
    */
    var builder = new UriBuilder("https://mysite.rocks/three");

    builder.Combine("one/two").Dump();
}

static class UriBuilderExtensions
{
    public static UriBuilder Combine(this UriBuilder builder, string path)
    {
        if (builder == null) return null;
        if (string.IsNullOrEmpty(path)) return builder;

        var delimiter = "/";
        var delimiterChar = '/';

        var baseSegments = builder.Uri.Segments
            .Where(i => i != delimiter)
            .Select(i => i.Trim(delimiterChar));
        var pathSegments = path.Split(delimiterChar)
            .Where(i => !string.IsNullOrEmpty(i));
        var combinedSegments = baseSegments.Union(pathSegments);

        builder.Path = string.Join(delimiter, combinedSegments);

        return builder;
    }
}