<Query Kind="Statements">
  <Namespace>System.Collections.ObjectModel</Namespace>
</Query>

var readOnlyCollection = new ReadOnlyCollection<int>(new List<int>(new[] { 1, 1, 3 }));

// you cannot do this: readOnlyCollection[1] = 2;

// these have no effect:
readOnlyCollection.Prepend(0);
readOnlyCollection.Append(4);

readOnlyCollection.Dump();

// 📖 [ https://docs.microsoft.com/en-us/dotnet/api/system.collections.objectmodel.readonlycollection-1?redirectedfrom=MSDN&view=netstandard-2.1 ]
// 📖 [ https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1?view=netstandard-2.1 ]
