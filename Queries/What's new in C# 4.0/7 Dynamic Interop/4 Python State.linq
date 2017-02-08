<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;System.Dynamic.dll</Reference>
  <Reference>&lt;ProgramFiles&gt; (x86)\IronPython 2.6\IronPython.dll</Reference>
  <Reference>&lt;ProgramFiles&gt; (x86)\IronPython 2.6\Microsoft.Scripting.Core.dll</Reference>
  <Reference>&lt;ProgramFiles&gt; (x86)\IronPython 2.6\Microsoft.Scripting.dll</Reference>
  <Namespace>System.Dynamic</Namespace>
  <Namespace>Microsoft.Scripting.Hosting</Namespace>
  <Namespace>IronPython.Hosting</Namespace>
  <Namespace>Microsoft.Scripting</Namespace>
</Query>

// The following string could come from a file or database:
string auditRule = "taxPaidLastYear / taxPaidThisYear > 2";

ScriptEngine engine = Python.CreateEngine ();    

ScriptScope scope = engine.CreateScope ();        
scope.SetVariable ("taxPaidLastYear", 20000m);
scope.SetVariable ("taxPaidThisYear", 8000m);

ScriptSource source = engine.CreateScriptSourceFromString (
					  auditRule, SourceCodeKind.Expression);

bool auditRequired = (bool) source.Execute (scope);
Console.WriteLine (auditRequired);
