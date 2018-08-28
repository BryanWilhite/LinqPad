<Query Kind="Statements" />

/*
    it should be clear (to me)
    that x and y are going to get smaller
    and smaller in the while loop;
    what is not so clear to me is
    what will happen as they approach zero
    (which is probably the point of the question)
*/
var x = 2437;
var y = 875;
while (x != y)
{
    if (x > y) x = x - y;
    if (x < y) y = y - x;
    new { x, y}.Dump("x, y");
}
