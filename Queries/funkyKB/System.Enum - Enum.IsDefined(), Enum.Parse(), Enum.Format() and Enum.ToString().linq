<Query Kind="Program" />

void Main()
{
    var input = "Two";
    
    var enumType = typeof(MyEnum);

    var enumIsDefinedValue = Enum.IsDefined(enumType, input);
    enumIsDefinedValue.Dump(nameof(enumIsDefinedValue));

    var enumParseValue = (MyEnum)Enum.Parse(enumType, input);
    enumParseValue.Dump(nameof(enumParseValue));

    var enumFormatValue = Enum.Format(enumType, enumParseValue, "f");
    enumFormatValue.Dump(nameof(enumFormatValue));

    var enumToStringValue = enumParseValue.ToString();
    enumToStringValue.Dump(nameof(enumToStringValue));
}

enum MyEnum
{
    One,
    Two,
    Three,
}