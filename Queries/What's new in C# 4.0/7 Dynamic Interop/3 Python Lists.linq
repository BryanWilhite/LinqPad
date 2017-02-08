<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;System.Dynamic.dll</Reference>
  <Reference>&lt;ProgramFiles&gt; (x86)\IronPython 2.6\IronPython.dll</Reference>
  <Reference>&lt;ProgramFiles&gt; (x86)\IronPython 2.6\Microsoft.Scripting.Core.dll</Reference>
  <Reference>&lt;ProgramFiles&gt; (x86)\IronPython 2.6\Microsoft.Scripting.dll</Reference>
  <Namespace>System.Dynamic</Namespace>
  <Namespace>Microsoft.Scripting.Hosting</Namespace>
  <Namespace>IronPython.Hosting</Namespace>
</Query>

string expr = "[1, 2, 3] + [4, 5]";

ScriptEngine engine = Python.CreateEngine();
engine.Execute (expr).Dump();
