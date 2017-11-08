<Query Kind="Statements" />

var left = new[]
{
    "three",
    "four",
    "five",
    "six"
};

var right = new[]
{
    "one",
    "two",
    "three",
    "four",
    "five"
};

left.Except(right).Dump("left-right");

right.Except(left).Dump("right-left");