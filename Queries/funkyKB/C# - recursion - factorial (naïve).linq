<Query Kind="Statements" />

int factorial(int n)
{
    int value;

    $"before: {nameof(n)}: {n}".Dump();

    switch (n)
    {
        case 0: // base case
            value = 0;
            break;
        case 1: // base case
            value = 1;
            break;
        default: // recursive case: this tail-call is augementing
            value = n * factorial(n - 1);
            break;
    }

    $"after: {nameof(n)}: {n}".Dump();
    return value;
}

factorial(3).Dump();

// 📖 https://en.wikipedia.org/wiki/Recursion_(computer_science)