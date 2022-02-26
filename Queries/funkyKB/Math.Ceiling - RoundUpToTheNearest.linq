<Query Kind="Program" />

void Main()
{
	RoundUpToTheNearest(36, 30).Dump();
}

public static decimal RoundUpToTheNearest(decimal input, decimal multiple)
{
	if (multiple == 0) throw new ArgumentException($"A {nameof(multiple)} of 0 is not possible.");

	return Math.Ceiling(input/multiple) * multiple;
}

// based on: https://stackoverflow.com/a/13153844/22944