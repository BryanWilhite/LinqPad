<Query Kind="Statements" />

var N = 100;
var n = 0;

int i;

void initialize()
{
    i = 0;
    n++;
}

bool doLogicGate()
{
    var test = i < N;
    n++;
    return test;
}

void increment()
{
    i++;
    n++;
}

for (initialize(); doLogicGate(); increment()) { }

(n == 2 * N + 2).Dump();

// 📖 https://docs.microsoft.com/en-us/archive/blogs/nmallick/how-to-calculate-time-complexity-for-a-given-algorithm