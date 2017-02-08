<Query Kind="Statements" />

/*

1.Write a program that prints the numbers from 1 to 100.
But for multiples of three print “JPL” instead of the number
and for the multiples of five print “NASA”.

For numbers which are multiples of both three and five print “JPL NASA”.

Implement in the language of your choice, or in pseudo-code.

*/

Enumerable
    .Range(1, 100)
    .Select(i =>
    {
        string JPL = "JPL", NASA = "NASA";
        
        Func<bool> isMultipleOf3 = () => ((i % 3) == 0);
        Func<bool> isMultipleOf5 = () => ((i % 5) == 0);
        
        if(isMultipleOf3() && isMultipleOf5()) return string.Format("{0} {1}", JPL, NASA);
        else if(isMultipleOf3()) return JPL;
        else if(isMultipleOf5()) return NASA;
        else return i.ToString();
    })
    .Dump();