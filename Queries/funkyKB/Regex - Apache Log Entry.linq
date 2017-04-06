<Query Kind="Statements" />

//https://dotnet-snippets.de/snippet/apache-log-file-parsen-regex/5969

var logEntryLine = @"180.76.15.159 - - [28/Feb/2017:19:00:12 -0500] ""GET /rasxlog/?feed=rss2&p=2616 HTTP/1.1"" 301 487 ""-"" ""Mozilla/5.0 (compatible; Baiduspider/2.0; +http://www.baidu.com/search/spider.html)""";

string logEntryPattern = "^([\\d.]+) (\\S+) (\\S+) \\[([\\w:/]+\\s[+\\-]\\d{4})\\] \"(.+?)\" (\\d{3}) (\\d+) \"([^\"]+)\" \"([^\"]+)\"";

Match regexMatch = Regex.Match(logEntryLine, logEntryPattern);

Console.WriteLine("IP Address: " + regexMatch.Groups[1].Value);
Console.WriteLine("Date&Time: " + regexMatch.Groups[4].Value);
Console.WriteLine("Request: " + regexMatch.Groups[5].Value);
Console.WriteLine("Response: " + regexMatch.Groups[6].Value);
Console.WriteLine("Bytes Sent: " + regexMatch.Groups[7].Value);
if (!regexMatch.Groups[8].Value.Equals("-"))
    Console.WriteLine("Referer: " + regexMatch.Groups[8].Value);
Console.WriteLine("Browser: " + regexMatch.Groups[9].Value);