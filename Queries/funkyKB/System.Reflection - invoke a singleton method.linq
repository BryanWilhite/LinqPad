<Query Kind="Program" />

void Main()
{
    var type = typeof(Singleton);
    MethodInfo method = type.GetMethod("AddNumbers");
    PropertyInfo prop = type.GetProperty("Instance",
        BindingFlags.Static | BindingFlags.Public);

    var instance = prop.GetValue(prop, Type.EmptyTypes);

    ((int)method.Invoke(instance, Type.EmptyTypes)).Dump();
}

sealed class Singleton
{
    private static readonly Singleton instance = new Singleton();

    private Singleton() { }

    public static Singleton Instance
    {
        get
        {
            return instance;
        }
    }

    public int AddNumbers()
    {
        return 41 + 1;
    }
}