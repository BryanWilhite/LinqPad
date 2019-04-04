<Query Kind="Program" />

void Main()
{
    var container = new MyContainerClass();
    container.DoThatThing();
}

class MyContainerClass
{
    public void DoThatThing()
    {
        var mine = new MyCallingClass();
        mine.DoMyThing();
    }
}

class MyCallingClass { }

static class MyExtensionMethodClass
{
    public static void DoMyThing(this MyCallingClass mine)
    {
        var stackFrame = new StackFrame(1);
        var method = stackFrame.GetMethod();
        method.Name.Dump("called from");
        method.DeclaringType.Dump("caller type");
    }
}

/*
    “Note: C# compilers some times optimizes the code
    and replaces method calls with inline code.
    So when such optimization is performed by C# compiler,
    the above described method fails to get the name of the calling method.
    In our requirement we were very sure that C# compiler is not going
    to do any such optimization and we used this code.”

    [https://www.techdreams.org/microsoft/c-how-to-get-calling-class-name-method-name-using-stackframe-class/5815-20110507]
*/
