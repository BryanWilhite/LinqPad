<Query Kind="Program">
  <Namespace>System.Net</Namespace>
</Query>

//https://stackoverflow.com/a/732110/22944
void Main()
{
    var html = "<a href=\"http://www.flickr.com/photos/wilhite/6705622765/in/photostream/\" title=\"Yoda Mug\"><img alt=\"Yoda Mug\" src=\"http://farm8.staticflickr.com/7006/6705622765_8d6e05522e_m.jpg\" style=\"float:right;margin:16px;\" /></a><p>I actually need this for my current work:</p><ul><li>I removed the <code>Songhay.DataAccess.Runner</code> namespace from the <code>Songhay.DataAccess</code> project.</li><li>I’ve renamed the old <code>Songhay.DataAccess.Runner</code> project to <code>Songhay.DataAccess.Runner-Console</code> (this might be a new convention going forward).</li><li>I’ve generated a new <code>Songhay.DataAccess.Runner</code> project with the removed assets from the <code>Songhay.DataAccess</code> project</li></ul><p>Now that the DAR stuff is cleanly separated from the <code>System.Data.Common</code> stuff, my generic data access routines that date back to .NET 2.0 is ready for the public.</p><a href=\"http://songhaydataaccess.codeplex.com/\" title=\"Songhay Data Access\" style=\"display:block;margin:16px;margin-left:auto;margin-right:auto;max-width:733px;\"><img alt=\"Songhay Data Access\" src=\"http://farm8.staticflickr.com/7154/6762510791_99721a18ff_o.png\" /></a><h3>Songhay Data Access</h3><p><a href=\"http://songhaydataaccess.codeplex.com/\">Songhay Data Access</a> is a set of static helper classes defining routines around <code>System.Data.Common</code>. As of this writing Songhay Data Access has been used with SQLite, MySQL and SQL Server, but, in theory, this set of libraries will work with <em>any</em> database that has an ADO.NET provider written for it.</p><p>Here are some of the highlights:</p><ul><li>Gets a <code>Common.DbConnection</code> object from a provider name and connection string (in <code>CommonDbms</code>).</li><li>Converts a generic Dictionary into an array of <code>IDataParameter</code> (in <code>CommonParameter</code>).</li><li>Returns a string or an object with an <code>IDbConnection</code> and a SQL statement (in <code>CommonScalar</code>).</li><li>Returns an instance of <code>IDataReader</code> with an <code>IDbConnection</code> and a SQL statement (in <code>CommonReader</code>).</li><li>Returns an <code>XPathDocument</code> with an <code>IDbConnection</code> and a SQL statement (in <code>CommonReader</code>).</li><li>Full support for parsing Nullable generics with overloads for custom default values (in <code>FrameworkTypeUtility</code>).</li></ul><p>Most “normal” .NET developers, starting out with .NET 1.x or 2.x, live almost exclusively in the world of <code>System.Data.SqlClient</code> to meet data-access needs. I was drawn to <code>System.Data.Common</code> from the very beginning because Microsoft’s work in this namespace represents ‘the sequel’ to <a href=\"http://en.wikipedia.org/wiki/ODBC\">ODBC</a>—of the olden days of <acronym title=\"Microsoft Component Object Model\">COM</acronym> when <a href=\"http://en.wikipedia.org/wiki/Don_Box\">Don Box</a> was but a young Jedi. Writing helpers for <code>System.Data.Common</code> seems like, um, “common” sense to me, while investing heavily in <code>System.Data.SqlClient</code> is a bold declaration that all databases on your watch will be SQL Server for ever more.</p><p>I was very pleased to find out that the <a href=\"http://msdn.microsoft.com/en-us/library/system.data.entityclient.entitycommand(v=vs.110).aspx\">Entity Framework</a> is based on <code>System.Data.Common</code>. Songhay Data Access is my tiny alternative to the Entity Framework, when I need to get as close to the <acronym title=\"Database Management System\">DBMS</acronym> as .NET will allow.</p>";
    StripHtml(html).Dump("stripped HTML");
}

static string StripHtml(string htmlText, bool htmlDecode = true)
{
    var markupRegex = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
    var spaceRegex = new Regex(@"\s+");
    var endCharsRegex = new Regex(@"\s+([.,\,\),—,–,-,:,;,’,”])");
    var result = markupRegex.Replace(htmlText, " ");
    result = spaceRegex.Replace(result, " ");
    result = endCharsRegex.Replace(result, "$1"); // see https://docs.microsoft.com/en-us/dotnet/standard/base-types/substitutions-in-regular-expressions
    
    return htmlDecode ? WebUtility.HtmlDecode(result) : result;
}
