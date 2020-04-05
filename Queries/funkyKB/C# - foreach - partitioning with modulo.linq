<Query Kind="Statements" />

var partitionList = new List<int>();
var partitionSize = 10;
var set = Enumerable.Range(1, 105);

var counter = 0;
foreach (var i in set)
{
	++counter;
	
	partitionList.Add(i);
	
	if(counter % partitionSize == 0)
	{
		partitionList.Dump("partitioning");

		partitionList.Clear();
	}
}

partitionList.Dump("partitioning [final]");
