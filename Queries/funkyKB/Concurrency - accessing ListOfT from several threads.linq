<Query Kind="Statements">
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

var list = new List<string>{"zero","one","two","three","four","five","six","seven","eight","nine","ten"};

list.Dump();

await Task.WhenAll(new[]{
Task.Factory.StartNew(()=> {list.Insert(3,"two.5"); ("0."+list[0]+"["+Thread.CurrentThread.ManagedThreadId.ToString()+"]").Dump();}),
Task.Factory.StartNew(()=> {("1."+list[1]+"["+Thread.CurrentThread.ManagedThreadId.ToString()+"]").Dump();}),
Task.Factory.StartNew(()=> {("2."+list[2]+"["+Thread.CurrentThread.ManagedThreadId.ToString()+"]").Dump();}),
Task.Factory.StartNew(()=> {("3."+list[3]+"["+Thread.CurrentThread.ManagedThreadId.ToString()+"]").Dump();}),
Task.Factory.StartNew(()=> {list.AddRange(new[]{"eleven","twelve","thirteen"}); ("4."+list[4]+"["+Thread.CurrentThread.ManagedThreadId.ToString()+"]").Dump();}),
Task.Factory.StartNew(()=> {("5."+list[5]+"["+Thread.CurrentThread.ManagedThreadId.ToString()+"]").Dump();}),
Task.Factory.StartNew(()=> {("6."+list[6]+"["+Thread.CurrentThread.ManagedThreadId.ToString()+"]").Dump();}),
Task.Factory.StartNew(()=> {list.AddRange(new[]{"fourteen","fifteen"});("7."+list[7]+"["+Thread.CurrentThread.ManagedThreadId.ToString()+"]").Dump();}),
Task.Factory.StartNew(()=> {("8."+list[8]+"["+Thread.CurrentThread.ManagedThreadId.ToString()+"]").Dump();}),
Task.Factory.StartNew(()=> {("9."+list[9]+"["+Thread.CurrentThread.ManagedThreadId.ToString()+"]").Dump();}),
});

list.Dump();