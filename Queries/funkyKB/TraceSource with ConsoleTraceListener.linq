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

/*
    There is a tendency to not make clean separation of concerns between
    trace listening and trace sourcing.
 
    One conservative move is to address trace sourcing first and use TraceSource
    and then look to third-party solutions to trace listening (via *.config declarations).
*/
