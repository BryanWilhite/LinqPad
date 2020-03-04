<Query Kind="Program" />

void Main()
{

}

private static bool IsWritable(MemberInfo info)
{
    var fieldInfo = info as FieldInfo;
    if (fieldInfo != null)
    {
        return fieldInfo.IsPublic;
    }

    var propertyInfo = info as PropertyInfo;
    return (propertyInfo != null) && propertyInfo.CanWrite;
}

private static bool IsWritable7x(MemberInfo memberInfo)
{
    switch (memberInfo)
    {
        case FieldInfo fieldInfo:
            return fieldInfo.IsPublic;
        case PropertyInfo propertyInfo:
            return propertyInfo.CanWrite;
        default:
            return false;
    }
}
