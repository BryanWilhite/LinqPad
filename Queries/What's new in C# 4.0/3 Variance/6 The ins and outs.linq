<Query Kind="Program" />

public interface IEnumerable <out T>
{
	IEnumerator<T> GetEnumerator();
}

public interface IComparer <in T>
{
	int Compare (T x, T y);	
}

void Main()
{
}