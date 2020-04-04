<Query Kind="Statements" />

var N = 100;
var n = 0;

void initialize(out int i)
{
    i = 0;
    n++;
}

bool doLogicGate(Func<bool> gate)
{
    n++;
    return gate.Invoke();
}

void increment(ref int i)
{
    i++;
    n++;
}

for (initialize(out var i); doLogicGate(() => i < N); increment(ref i)) { }

(n == 2 * N + 2).Dump($"{nameof(n)}: {n}");

// 📖 https://docs.microsoft.com/en-us/archive/blogs/nmallick/how-to-calculate-time-complexity-for-a-given-algorithm

n = 0;

for (initialize(out var i); doLogicGate(() => i < N); increment(ref i))
{
    for (initialize(out var j); doLogicGate(() => j < N); increment(ref j)) { }
}

(n == (2 * N + 2) + ((2 * N + 2) * N)).Dump($"{nameof(n)}: {n:#,###}");
