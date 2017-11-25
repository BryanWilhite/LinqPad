<Query Kind="Program" />

void Main()
{
    var subclass = new MySubClass();
    subclass.GetEnglishNumber("dos").Dump();
}

class MySubClass : MyBase
{
    protected override void OnSetupSet(Dictionary<string, string> set)
    {
        set.Add("uno", "one");
        set.Add("dos", "two");
        set.Add("tres", "three");

        base.OnSetupSet(set);
    }
}

abstract class MyBase
{
    public MyBase()
    {
        this._set = new Dictionary<string, string>();
        this.OnSetupSet(this._set);
    }
    
    public string GetEnglishNumber(string spanishKey)
    {
        return this._set[spanishKey];
    }

    protected virtual void OnSetupSet(Dictionary<string, string> set)
    {
        set.Dump();
    }

    Dictionary<string, string> _set;
}