<Query Kind="Statements" />

int[,] m =
{
    { 12, 11, 10, 9 },
    { 8, 7, 6, 5 },
    { 4, 3, 2, 1 },
};

var r = 0;
foreach (var element in m)
{
    if(r % 4 == 0) "row".Dump();
    element.Dump();
    r++;
}