<Query Kind="Statements" />

var set = new[]
{
    new KeyValuePair<int, string>(1, "one"),
    new KeyValuePair<int, string>(2, "two"),
    new KeyValuePair<int, string>(3, "three"),
    new KeyValuePair<int, string>(4, "four"),
    new KeyValuePair<int, string>(5, "five"),
    new KeyValuePair<int, string>(6, "six"),
    new KeyValuePair<int, string>(7, "seven"),
};

var iterator = set.OfType<double>();

Assert.IsTrue(iterator is IEnumerable<double>, "The expected interface type is not here.");
Assert.IsFalse(iterator.Any(), "No results were expected.");
