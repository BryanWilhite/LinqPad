<Query Kind="Statements">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

var compositeTasks = (new[] { 1, 2, 3, 4 })
	.Select(async i =>
		{
			var x = await Task.FromResult(i);
			var y = await Task.FromResult(x * 2);

			return await Task.FromResult(x + y);
		})
	.ToArray();

Task.WaitAll(compositeTasks);

compositeTasks.Select(i => i.Result).ToArray().Dump("results");
