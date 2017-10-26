<Query Kind="Statements" />

var x = "1.5"; //a decimal string
int y;
try
{
    y = Convert.ToInt32(x);
}
catch (Exception ex)
{
    ex.Message.Dump("expected exception");
}
