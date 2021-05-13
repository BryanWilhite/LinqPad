<Query Kind="Expression" />

//KeyValuePair has its own  default equality comparer
//so using it with Distinct can be a big help:
new []
{
    new KeyValuePair<int, string>(1, "one"),
    new KeyValuePair<int, string>(1, "one"),
    new KeyValuePair<int, string>(1, "one"),
    new KeyValuePair<int, string>(1, "one"),
    new KeyValuePair<int, string>(1, "one"),
    new KeyValuePair<int, string>(1, "one"),
    new KeyValuePair<int, string>(2, "two"),
}.Distinct()