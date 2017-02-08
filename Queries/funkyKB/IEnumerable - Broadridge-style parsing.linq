<Query Kind="Statements" />

var data = "DATA HEADERZ104 ENDZ105 ENDZ106 END";
var initialIndex = 11;
var segmentIncrement = 8;

var segmentCount = (data.Length - initialIndex) / segmentIncrement;

(new [] {0})
    .Select(x => data.Substring(0, initialIndex))
    .Union
    (
        Enumerable
            .Range(1, segmentCount)
            .Select((x, i) =>
            {
                var startIndex = initialIndex + (segmentIncrement * i);
                return data.Substring(startIndex, segmentIncrement);
            })
    )
.Dump();