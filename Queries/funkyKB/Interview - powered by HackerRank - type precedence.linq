<Query Kind="Statements" />

int a = 10;
double b = 10.1;
float c = 10.1f;
long d = 10;

((a + d) * (c + b)).GetType().Dump();

/*
    I'm surprised it hasn't been said already,
    float is a C# alias keyword and isn't a .Net type.
    it's System.Single..
    single and double are floating binary point types. – Brett Caswell
    [https://stackoverflow.com/a/618596/22944]
*/

/*
    Precision is the main difference. 
    Float - 7 digits (32 bit)
    Double-15-16 digits (64 bit)
    Decimal -28-29 significant digits (128 bit)
    [https://stackoverflow.com/a/618542/22944]
*/

/*
    CS0019 Operator '+' cannot be applied to operands of type 'float' and 'decimal'
*/
