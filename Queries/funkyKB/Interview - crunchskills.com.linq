<Query Kind="Statements" />

/*
    “Most common phone interview question at google.”
    [📖 https://crunchskills.com/most-common-interview-question-at-google/ ]

    Given an arbitrary string of characters eg. ‘aabcdef’;
    Return the first recurring character.
*/

var characters = "aabcdef";
var set = new HashSet<char>(characters.Length);

foreach (char c in characters)
{
    $"char: {c}".Dump();
    if(set.Contains(c))
    {
        c.Dump("recurring character");
        break;
    }
    else set.Add(c);
}

/*
    This question is answered with the knowledge of the hash table, how it is implmented.

    It is a quick and merciful method to test for this knowledge.

    The use of a dedicated data structure to execute a combinatorial search
    appears to be preferred over a time-complex sequential scan.
*/