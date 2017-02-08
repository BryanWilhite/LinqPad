<Query Kind="Statements">
  <NuGetReference>MoreLinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

var input = "Microsoft Windows";
input.Dump("input");

var placeholder = "*";

var duplicates = input
    .ToCharArray() //would never have remembered this on the call!
    .Select((c,i) => new { Char=c, Index=i })
    .GroupBy(i => i.Char) //should have remembered this!
    .Where(i => i.Count() > 1)
    .ToArray();

duplicates.Dump("duplicate data");

var output = input;

duplicates.ForEach(i =>
{
    i.Skip(1).ForEach(j =>
    {
        output = (j.Index < output.Length) ?
            output.Insert(j.Index, placeholder)
            :
            string.Concat(output, placeholder);
        output = output.Remove(j.Index + 1, 1);
    });
});

output.Dump("output with placeholder");

output.Replace(placeholder, string.Empty).Dump("final output");

(new string(input.ToCharArray().Distinct().ToArray())).Dump("one line alternative!");