<Query Kind="Program" />

void Main()
{
    FrameworkTypeUtility
        .GetValueFromConstant<MyMagicStrings>("MyMagicStrings.MY_MAGIC_STRING")
        .Dump("from class");

    FrameworkTypeUtility
        .GetValueFromConstant<MyFasterMagicStrings>("MyFasterMagicStrings.MY_OTHER_MAGIC_STRING")
        .Dump("from struct");
}

public static class FrameworkTypeUtility
{
    public static string GetValueFromConstant<T>(string key)
    {
        var type = typeof(T);
        if(string.IsNullOrEmpty(key)) return null;
        
        var keyArray = key.Split('.');
        var field = type.GetField(keyArray.Last());
        return (string)field.GetRawConstantValue();
    }
}

class MyMagicStrings
{
    public const string MY_MAGIC_STRING = "it is magic";
    public const string MY_OTHER_MAGIC_STRING = "yes, it is";
}

struct MyFasterMagicStrings
{
    public const string MY_MAGIC_STRING = "it is faster magic";
    public const string MY_OTHER_MAGIC_STRING = "yes, it is faster";
}