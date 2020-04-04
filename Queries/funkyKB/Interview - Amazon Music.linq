<Query Kind="Statements" />

/*
    find the clusters defined by:

    * adjacent `1`s
    * `1` on a row by itself
    * a row ending with adjacent `1`s, followed by a row starting with adjacent `1`s

    the 2D array below has `3` clusters:
*/

int[,] m =
{
    { 1, 1, 0, 0 },
    { 0, 0, 1, 0 },
    { 0, 0, 0, 0 },
    { 0, 0, 1, 1 },
    { 1, 1, 0, 0 },
};

IEnumerable<int> reduce(IEnumerable<int> collection)
{
    int previous = -1;
    foreach (var current in collection)
    {
        if(previous != current) yield return current;
        previous = current;
    }
}

IEnumerable<int> mCollection = m.OfType<int>().ToArray();
// 📖 https://stackoverflow.com/questions/5132397/fast-way-to-convert-a-two-dimensional-array-to-a-list-one-dimensional

IEnumerable<int> reduction = reduce(mCollection);

int numberOfClusters = reduction.Sum();
numberOfClusters.Dump(nameof(numberOfClusters));
