<Query Kind="Statements" />

var location = "./";
Uri.IsWellFormedUriString(location, UriKind.Relative).Dump(location);

location = "./test";
Uri.IsWellFormedUriString(location, UriKind.Relative).Dump(location);

location = "./test/";
Uri.IsWellFormedUriString(location, UriKind.Relative).Dump(location);

location = "./test#";
Uri.IsWellFormedUriString(location, UriKind.Relative).Dump(location);

location = "http://wordwalkingstick.com/test/#/segment/99";
Uri.IsWellFormedUriString(location, UriKind.Absolute).Dump(location);

var uri = new Uri(location, UriKind.Absolute);

var fragment = uri.Fragment;
var fragmentArray = fragment.Split('/');

(3 == fragmentArray.Length).Dump("expected number of segments");

location = "/segment/99"; //form of a Silverlight bookmark
Uri.IsWellFormedUriString(location, UriKind.Relative).Dump(location);

uri = new Uri(location, UriKind.Relative);
var relativeUriSegments = uri.OriginalString.Split('/'); //FUNKYKB: Uri.Segments does not support Relative URIs.
(3 == relativeUriSegments.Length).Dump("expected number of segments");

Uri.IsWellFormedUriString(location, UriKind.RelativeOrAbsolute).Dump(location);
uri = new Uri(location, UriKind.RelativeOrAbsolute);
relativeUriSegments = uri.OriginalString.Split('/');
(3 == relativeUriSegments.Length).Dump("expected number of segments");
