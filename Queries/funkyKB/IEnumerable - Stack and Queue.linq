<Query Kind="Statements" />

var stack = new Stack<string>();

stack.Push("one");
stack.Push("two");
stack.Push("three");
stack.Push("four");

stack.Dump();

stack.Take(1).First().Dump("Stack Take(1)");
stack.ElementAt(1).Dump("Stack ElementAt(1)");

var queue = new Queue<string>();

queue.Enqueue("one");
queue.Enqueue("two");
queue.Enqueue("three");
queue.Enqueue("four");

queue.Dump();

queue.Take(1).First().Dump("Queue Take(1)");
queue.ElementAt(1).Dump("Queue ElementAt(1)");
