<Query Kind="Statements" />

/*
This exercise reveals the power of reference objects
and the purpose of the “linked list.”

i) grab on to the head of the linked list
ii) refer to next item after the head and remove it
iii) add the value of this reference to the first position of the linked list; go to (ii)

see: http://stackoverflow.com/questions/8686168/reversing-single-linked-list-in-c-sharp
also: http://stackoverflow.com/questions/15563043/when-is-doubly-linked-list-more-efficient-than-singly-linked-list

*/

var linkedList = new LinkedList<int>(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
linkedList.Dump("initial state");

var head = linkedList.First;
while (head.Next != null)
{
    var next = head.Next;
    linkedList.Remove(next);
    linkedList.AddFirst(next.Value);
}

linkedList.Dump("final state");