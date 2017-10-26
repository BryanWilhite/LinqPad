<Query Kind="Statements" />

var input = 603.625M;

var output = Math.Round(input, 2, MidpointRounding.AwayFromZero);

(603.63M == output).Dump("should be equal");

input = 129.195M;

output = Math.Round(input, 2, MidpointRounding.AwayFromZero);

(129.20M == output).Dump("should be equal");
