<Query Kind="Program" />

void Main()
{
    var input = "Two";

    var enumIsDefinedValue = Enum.IsDefined(typeof(MyEnum), input);
    enumIsDefinedValue.Dump(nameof(enumIsDefinedValue));

    var enumParseValue = Enum.Parse(typeof(MyEnum), input);
    enumParseValue.Dump(nameof(enumParseValue));

    var enumFormatValue = Enum.Format(typeof(MyEnum), enumParseValue, "f");
    enumFormatValue.Dump(nameof(enumFormatValue));
}

enum MyEnum
{
    One,
    Two,
    Three,
}