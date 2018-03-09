<Query Kind="Program" />

void Main()
{
    typeof(MyClass).GetYuml().Dump();
}

static class TypeExtenions
{
    public static string GetYuml(this Type type)
    {
        string getAnyDetails()
        {
            var fields = type
                .GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .OfType<FieldInfo>()
                .Where(i => !i.Name.EndsWith("__BackingField"));
            fields.Dump($"{nameof(FieldInfo)}");

            var methods = type
                .GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .OfType<MethodInfo>()
                .Where(i => (i.DeclaringType == type) && !i.IsSpecialName);
            methods.Dump($"{nameof(MethodInfo)}");

            var properties = type
                .GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .OfType<PropertyInfo>();
            properties.Dump($"{nameof(PropertyInfo)}");

            var builder = new StringBuilder();

            if (properties.Any()) builder.Append("|");
            foreach (var info in properties)
            {
                if (info.GetMethod.IsFamily) builder.Append("#");
                else if (info.GetMethod.IsPrivate) builder.Append("-");
                else if (info.GetMethod.IsPublic) builder.Append("+");
                builder.AppendFormat($"{info.Name};");
            }

            if (methods.Any()) builder.Append("|");
            foreach (var info in methods)
            {
                if (info.IsFamily) builder.Append("#");
                else if (info.IsPrivate) builder.Append("-");
                else if (info.IsPublic) builder.Append("+");
                builder.AppendFormat($"{info.Name}();");
            }

            if (fields.Any()) builder.Append("|");
            foreach (var info in fields)
            {
                if (info.IsFamily) builder.Append("#");
                else if (info.IsPrivate) builder.Append("-");
                else if (info.IsPublic) builder.Append("+");
                builder.AppendFormat($"{info.Name};");
            }

            return builder.ToString();
        }

        var yUml = $"[{type.Name}{getAnyDetails()}]";
        return yUml;
    }
}

public class MyClass
{
    public MyClass()
    {
        myField = "Hello world.";
    }
    public int MyProperty { get; set; }

    public string MyOtherProperty { get; set; }

    protected string MyProtectedProperty { get; set; }

    public string GetHello()
    {
        return myField;
    }

    readonly string myField;
}
