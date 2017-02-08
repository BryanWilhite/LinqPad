<Query Kind="Statements" />

#define TRACE

var traceSource = new TraceSource("LINQpad5");
traceSource.Switch.Level = SourceLevels.All; //this line is very important!

traceSource.Listeners.Remove("Default");

using (var consoleListener = new ConsoleTraceListener(useErrorStream: false))
{
    traceSource.Listeners.Add(consoleListener);
    traceSource.TraceInformation("Hello world!");
}