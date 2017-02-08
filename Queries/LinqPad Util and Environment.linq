<Query Kind="Statements" />

Path.GetDirectoryName(Util.CurrentQueryPath).Dump("Query Folder");

Environment.CurrentDirectory.Dump("Environment.CurrentDirectory");

Environment.GetEnvironmentVariable("ALLUSERSPROFILE").Dump("ALLUSERSPROFILE");

Environment.GetEnvironmentVariable("APPDATA").Dump("APPDATA");

Environment.GetEnvironmentVariable("COMPUTERNAME").Dump("COMPUTERNAME");

Environment.GetEnvironmentVariable("LOCALAPPDATA").Dump("LOCALAPPDATA");

Environment.GetEnvironmentVariable("OneDrive").Dump("OneDrive");

Environment.GetEnvironmentVariable("USERPROFILE").Dump("USERPROFILE");

Environment.GetEnvironmentVariables().Dump("Environment.GetEnvironmentVariables()");