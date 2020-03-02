<Query Kind="Program" />

void Main()
{
    object instance = Activator.CreateInstance(typeof(MyBigClass));

    instance
        .GetType()
        .GetMembers()
        .Where(MembersPredicate)
        .SelectMany(MembersProjection)
        .ToDictionary(i => i.Key, j => j.Value)
        .Dump();
}

static bool MembersPredicate(MemberInfo info)
{
    var fieldInfo = info as FieldInfo;
    if (fieldInfo != null) return fieldInfo.IsPublic;

    var propertyInfo = info as PropertyInfo;
    return (propertyInfo != null) && propertyInfo.CanWrite;
}

static IEnumerable<KeyValuePair<string, Type>> MembersProjection(MemberInfo info)
{
    var fieldInfo = info as FieldInfo;
    if (fieldInfo != null)
        return fieldInfo
            .FieldType
            .GenericTypeArguments
            .Select(i => new KeyValuePair<string, Type>(fieldInfo.Name, i));

    var propertyInfo = info as PropertyInfo;
    return propertyInfo
        .PropertyType
        .GenericTypeArguments
        .Select(i => new KeyValuePair<string, Type>(propertyInfo.Name, i));
}

public class MyLittleClass<T>
{
    T Value;
}

public class MyBigClass
{
    public MyLittleClass<int> MyField;

    public MyLittleClass<string> MyProperty { get; set; }

    public MyLittleClass<decimal> MyOtherProperty { get; set; }
}