<Query Kind="Statements" />

/*
    The URI equivalent of System.IO.Path.Combine()
    is this UriBuilder pattern:
*/
var builder = new UriBuilder("https://mysite.rocks/");
builder.Path = "/one/two/";
builder.Uri.Dump();