<Query Kind="Program">
  <NuGetReference>NUnitLite</NuGetReference>
  <Namespace>NUnitLite</Namespace>
  <Namespace>NUnit.Framework</Namespace>
</Query>

void Main()
{
    var specialFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    var workingFolder = $@"{specialFolder}\NUnitLiteTestResults";
    var args = new[]
        {
            "--labels=All",
            "--nocolor",
            "--noheader",
            $"--work={workingFolder}"
        };

    try
    {
        (new AutoRun()).Execute(args);
    }
    catch (NullReferenceException ex)
    {
        ex.Dump("why?");
    }
}

public class Runner
{
    /*
        public static int Main(string[] args) {
            return new AutoRun(Assembly.GetCallingAssembly()).Execute(new String[] {"--labels=All"});
        }
    */
    [TestFixture]
    public class FooTest
    {
        [Test]
        public void ShouldCheckBoolean()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void ShouldCompareNumbers()
        {
            Assert.AreEqual(2, 2);
        }

        [Test]
        public void ShouldCompareStrings()
        {
            Assert.AreEqual("abc", "abc");
        }

        [Test]
        public void TestCompareLists()
        {
            Assert.AreEqual(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 });
        }
    }
}

/*
    refs:
    - https://github.com/nunit/docs/wiki/NUnitLite-Runner
    - https://github.com/nunit/docs/wiki/Console-Command-Line
    - https://github.com/cjdev/interview-preparation/blob/master/coderpad-testing.md#c-2
*/