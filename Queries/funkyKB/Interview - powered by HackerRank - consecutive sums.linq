<Query Kind="Statements" />

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

int[] P = Enumerable.Range(1, 125).ToArray();

var upperBound = P.Max();

int[] GetSubArray(int[] c, int lowerBound)
{
    IEnumerable<int> subArray = Enumerable.Empty<int>();

    var sum = 0;
    var i = 1;
    do
    {
        i = 1;
        do
        {
            subArray = c.Skip(lowerBound).Take(i);
            sum = subArray.Sum();
            ++i;
        } while (sum < upperBound);

        ++lowerBound;
    } while (sum != upperBound);

    return subArray.ToArray();
}

int[] root(int[] data) => data;
bool reject(int[] data, int[] c)
{
    if(c.Equals(data)) return false;
    var test = c.Sum() != upperBound;
    test.Dump("reject?");
    return test;
};
bool accept(int[] data, int[] c) => c.Sum() == upperBound;
void output(int[] data, int[] c) => c.Dump("output");
int[] first(int[] data, int[] c) => GetSubArray(data, lowerBound: (c.First() - 1));
int[] next(int[] data, int[] c) => GetSubArray(data, lowerBound: c.First());

void bt(int[] c)
{
    if (reject(P, c)) return;
    if (accept(P, c))
    {
        output(P, c);
        return;
    }

    var s = first(P, c);
    do
    {
        bt(s);
        s = next(P, s);
    } while (s.Length > 1);
}

bt(root(P));

/*
    procedure bt(c)
      if reject(P,c) then return
      if accept(P,c) then output(P,c)
      s ← first(P,c)
      while s ≠ Λ do     // Λ means null
        bt(s)
        s ← next(P,s)

    invocation: bt(root(P))

    root(P): return the partial candidate at the root of the search tree.
    reject(P,c): return true only if the partial candidate c is not worth completing.
    accept(P,c): return true if c is a solution of P, and false otherwise.
    first(P,c): generate the first extension of candidate c.
    next(P,s): generate the next alternative extension of a candidate, after the extension s.
    output(P,c): use the solution c of P, as appropriate to the application.

    [https://en.wikipedia.org/wiki/Backtracking]
    
    There is another example that appears to use Candidate and Data objects:
    [https://kunuk.wordpress.com/2012/12/25/backtracking-subset-sum-with-c/]

    Here is my first attempt unsing LINQ:

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

*/
