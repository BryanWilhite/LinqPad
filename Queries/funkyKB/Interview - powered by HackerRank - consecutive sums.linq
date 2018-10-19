/*
    find the number of ways to represent
    a long integer as a sum of two
    or more consecutive positive integers
    
    21 => [1,2,3,4,5,6]
          [6,7,8]
          [10,11]

    100 => [9,10,11,12,13,14,15,16]
           [18,19,20,21,22]

    125 => [8,9,10,11,12,13,14,15,16,17]
           [23,24,25,26,27]
           [62,63]

    see:
        https://math.stackexchange.com/questions/139842/in-how-many-ways-can-a-number-be-expressed-as-a-sum-of-consecutive-numbers
        https://www.geeksforgeeks.org/print-possible-sums-consecutive-numbers-sum-n/
*/

var integer = 125;
var solutions = new Dictionary<int, int>();

void FindSolution(int lowerBound)
{
    Enumerable.Range(lowerBound, integer).Aggregate((a, n) =>
    {
        // (new { a, n }).Dump();
        var upperBound = n - 1;

        if ((lowerBound != upperBound) && (a == integer))
            solutions.Add(lowerBound, upperBound);

        return a + n;
    });
}

Enumerable
    .Range(1, integer)
    .ToList()
    .ForEach(i => FindSolution(lowerBound: i));

solutions.Dump(nameof(solutions));