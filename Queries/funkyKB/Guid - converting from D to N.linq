<Query Kind="Statements" />

const string D = "D"; //b66e5f27-8c52-4d9e-aa8f-bfeca6349808
const string N = "N"; //B66E5F278C524D9EAA8FBFECA6349808

var guid = Guid.NewGuid();
var guidString = guid.ToString(D);
guidString.Dump(D);

Guid.ParseExact(guidString, D)
    .ToString(N)
    .ToUpperInvariant()
    .Dump(N);