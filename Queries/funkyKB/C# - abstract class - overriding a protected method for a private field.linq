<Query Kind="Program" />

void Main()
{
    var subclass = new MySubClass();
    subclass.GetEnglishNumber("dos").Dump();

    var subclass2 = new MyOtherSubClass();
    subclass2.GetEnglishNumber("dos").Dump();
}

class MySubClass : MyBase
{
    protected override void OnSetupSet(ref Dictionary<string, string> set)
    {
        set.Add("uno", "one");
        set.Add("dos", "two");
        set.Add("tres", "three");

        base.OnSetupSet(ref set);
    }
}

class MyOtherSubClass : MyBase
{
    protected override void OnSetupSet(ref Dictionary<string, string> set)
    {
        set = new Dictionary<string, string>
        {
            { "dos", "two" }
        };

        base.OnSetupSet(ref set);
    }
}

abstract class MyBase
{
    public MyBase()
    {
        this._set = new Dictionary<string, string>();
        this.OnSetupSet(ref this._set);
    }
    
    public string GetEnglishNumber(string spanishKey)
    {
        return this._set[spanishKey];
    }

    protected virtual void OnSetupSet(ref Dictionary<string, string> set)
    {
        this._set.Dump();
    }

    readonly Dictionary<string, string> _set;
}
/*
    This is a seductive way to violate encapsulation.
    With the ref keyword in use, this sample runs.
    Without the ref keyword, MyOtherSubClass.GetEnglishNumber() throws an exception.
    BTW: is the readonly keyword working correctly?
*/
