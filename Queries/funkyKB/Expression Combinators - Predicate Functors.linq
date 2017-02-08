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

public static class FuncSeed
{
    public static Func<T, bool> True<T>() { return f => true;  }
    
    public static Func<T, bool> False<T>() { return f => false; }
}

void Main()
{
    var data = new[]{2,4,6,8,11,43,65};
    var greaterThanFourAndLessThanForty = FuncSeed.True<int>()
        .And<int>(i => i > 4)
        .And<int>(i => i < 40);
    data.Where(greaterThanFourAndLessThanForty).Dump("Greater Than Four and Less Than Forty");
    
    var notGreaterThanFourAndLessThanForty = FuncSeed.True<int>()
        .AndNot(greaterThanFourAndLessThanForty);
    data.Where(notGreaterThanFourAndLessThanForty).Dump("Not Greater Than Four and Less Than Forty");
}