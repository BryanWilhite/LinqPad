<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var tasks = new[]
	{
		Task.Run (() => MyClass.GetOrdinal().Dump(Thread.CurrentThread.ManagedThreadId.ToString())),
		Task.Run (() => MyClass.GetOrdinal().Dump(Thread.CurrentThread.ManagedThreadId.ToString())),
		Task.Run (() => MyClass.GetOrdinal().Dump(Thread.CurrentThread.ManagedThreadId.ToString())),
		Task.Run (() => Enumerable.Range(0, 5).Aggregate((a, i) => {MyClass.GetOrdinal().Dump(Thread.CurrentThread.ManagedThreadId.ToString()); return i;})),
		Task.Run (() => MyClass.GetOrdinal().Dump(Thread.CurrentThread.ManagedThreadId.ToString())),
	};

	Task.WaitAll(tasks);

}

static class MyClass
{
	public static int GetOrdinal() => ++myInt;

	[ThreadStatic]
	private static int myInt;
}