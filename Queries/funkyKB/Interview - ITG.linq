<Query Kind="Program" />

void Main()
{
    var i = findString("ba", "ccccccc"); //returns -1 

    i.Dump("result");
}

// Define other methods and classes here
static int findString(string matchSet, string inputString) 
{ 
    var matchPairs = matchSet
        .ToCharArray()
        .Select((x,i) => new KeyValuePair<int, char>(i,x));
    matchPairs.Dump("matchPairs");

    var matchSetLength = matchSet.Length;
    
    var inputSets = inputString
        .ToCharArray()
        .Select((x,i) => 
        {
            var set = ((inputString.Length - i) >= matchSetLength) ?
                inputString.Substring(i, matchSetLength) : inputString.Substring(i);
            return new KeyValuePair<int, string>(i, set);
        });
    inputSets.Dump("inputSets");
    
    var first = inputSets
        .FirstOrDefault(i =>
            i.Value.ToCharArray().OrderBy(j => j)
                .SequenceEqual(matchSet.ToCharArray().OrderBy(j => j)));   

    return (first.Value != null)? first.Key : -1;
}