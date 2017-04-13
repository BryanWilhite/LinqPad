<Query Kind="Program" />

public static class FuncExtensions
{
    public static Func<T, bool> And<T>(this Func<T, bool> thisPredicate, Func<T, bool> predicate)
    {
        return a => thisPredicate(a) && predicate(a);
    }

    public static Func<T, bool> AndNot<T>(this Func<T, bool> thisPredicate, Func<T, bool> predicate)
    {
        return a => thisPredicate(a) && !predicate(a);
    }

    public static Func<T, bool> Or<T>(this Func<T, bool> thisPredicate, Func<T, bool> predicate)
    {
        return a => thisPredicate(a) || predicate(a);
    }

    public static Func<T, bool> OrNot<T>(this Func<T, bool> thisPredicate, Func<T, bool> predicate)
    {
        return a => thisPredicate(a) || !predicate(a);
    }
}

public static class FuncCombinator
{
    public static Func<T, bool> Combine<T>(bool @bool = true) { return f => @bool; }
}

void Main()
{
    var data = new[] { 2, 4, 6, 8, 11, 43, 65 };
    var greaterThanFourAndLessThanForty = FuncCombinator.Combine<int>()
        .And<int>(i => i > 4)
        .And<int>(i => i < 40);
    data.Where(greaterThanFourAndLessThanForty).Dump("Greater Than Four and Less Than Forty");

    var notGreaterThanFourAndLessThanForty = FuncCombinator.Combine<int>()
        .AndNot(greaterThanFourAndLessThanForty);
    data.Where(notGreaterThanFourAndLessThanForty).Dump("Not Greater Than Four and Less Than Forty");
}