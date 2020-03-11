<Query Kind="Program">
  <NuGetReference>xunit</NuGetReference>
  <Namespace>Xunit</Namespace>
</Query>

void Main()
{
}

public class Tests
{
    [Theory]
    [MemberData(nameof(GetNumbers))]
    public void AllNumbers_AreOdd_WithMemberData(int a, int b, int c, int d)
    {
        bool IsOddNumber(int number)
        {
            return number % 2 != 0;
        }

        Assert.True(IsOddNumber(a));
        Assert.True(IsOddNumber(b));
        Assert.True(IsOddNumber(c));
        Assert.True(IsOddNumber(d));
    }

    static IEnumerable<object[]> GetNumbers()
    {
        yield return new object[] { 5, 1, 3, 9 };
        yield return new object[] { 7, 1, 5, 3 };
    }
}


/*
    “MemberData gives us the same flexibility [as `ClassData`] but without the need for a class.”

    📖 [ https://hamidmosalla.com/2017/02/25/xunit-theory-working-with-inlinedata-memberdata-classdata/ ]
*/
