<Query Kind="Program" />

// https://www.geeksforgeeks.org/insertion-sort/
void Main()
{
	(new[] { 12, 22, 3, 7 }).DoInsertionSort();
}

public static class IListOfTExtensions
{
	public static IEnumerable<int> DoInsertionSort(this IList<int> list)
	{
		for (var i = 1; i < list.Count(); i++)
		{
			var current = list.ElementAt(i);
			var searchIndex = i - 1;

			while ((searchIndex >= 0) && (list.ElementAt(searchIndex) > current))
			{
				list[searchIndex + 1] = list.ElementAt(searchIndex);
				--searchIndex;
			}
			
			list[searchIndex + 1] = current;

			list.Dump(nameof(list));
		}

		return list;
	}
}

// see https://stackoverflow.com/questions/32664/is-there-a-constraint-that-restricts-my-generic-method-to-numeric-types
