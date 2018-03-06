<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

var jO = JObject.Parse(@"
{
    ""data"": [
        {
            ""MY_FIELD_ONE"": ""data-one-thing"",
            ""MY_FIELD_TWO"": ""data-one-other-thing"",
            ""MY_FIELD_THREE"": ""data-one-other-other-thing""
        },
        {
            ""MY_FIELD_ONE"": ""data-two-thing"",
            ""MY_FIELD_TWO"": ""data-two-other-thing"",
            ""MY_FIELD_THREE"": ""data-two-other-other-thing""
        },
        {
            ""MY_FIELD_ONE"": ""data-three-thing"",
            ""MY_FIELD_TWO"": ""data-three-other-thing"",
            ""MY_FIELD_THREE"": ""data-three-other-other-thing""
        }
    ]
}
");

var jA = jO["data"].Value<JArray>();
var fields = new
{
    myFieldOne = "MY_FIELD_ONE",
    myFieldTwo = "MY_FIELD_TWO",
    myFieldThree = "MY_FIELD_THREE"
};
var builder = new StringBuilder();

// concatenation with C# 6 string interpolation:
foreach (var datum in jA)
{
    builder.Append($@"
insert into
    MY_TABLE
(
    {fields.myFieldOne},
    {fields.myFieldTwo},
    {fields.myFieldThree}
)
values
(
    '{datum[fields.myFieldOne].Value<string>()}',
    '{datum[fields.myFieldTwo].Value<string>()}',
    '{datum[fields.myFieldThree].Value<string>()}'
)
;
");
}
builder.ToString().Dump("C# 6 string interpolation");

builder.Clear();

// concatenation with C# 5 limitations:
foreach (var datum in jA)
{
    builder.AppendLine("insert into");
    builder.AppendLine("    MY_TABLE");
    builder.AppendLine("(");
    builder.AppendFormat("    {0},", fields.myFieldOne);
    builder.AppendLine();
    builder.AppendFormat("    {0},", fields.myFieldTwo);
    builder.AppendLine();
    builder.AppendFormat("    {0}", fields.myFieldThree);
    builder.AppendLine();
    builder.AppendLine(")");
    builder.AppendLine("values");
    builder.AppendLine("(");
    builder.AppendFormat("    '{0}',", datum[fields.myFieldOne].Value<string>());
    builder.AppendLine();
    builder.AppendFormat("    '{0}',", datum[fields.myFieldTwo].Value<string>());
    builder.AppendLine();
    builder.AppendFormat("    '{0}'", datum[fields.myFieldThree].Value<string>());
    builder.AppendLine();
    builder.AppendLine(")");
    builder.AppendLine(";");
    builder.AppendLine();
}
builder.ToString().Dump("C# 5 limitations");
builder.Clear();

// concatenation with C# 5 limitations (alternative):
foreach (var datum in jA)
{
    var lines = new[] {
        "insert into",
        "    MY_TABLE",
        "(",
        string.Format("    {0},", fields.myFieldOne),
        string.Format("    {0},", fields.myFieldTwo),
        string.Format("    {0}", fields.myFieldThree),
        ")",
        "values",
        "(",
        string.Format("    '{0}',", datum[fields.myFieldOne].Value<string>()),
        string.Format("    '{0}',", datum[fields.myFieldTwo].Value<string>()),
        string.Format("    '{0}'", datum[fields.myFieldThree].Value<string>()),
        ")",
        ";",
    };
    var joined = string.Join(Environment.NewLine, lines);
    builder.AppendLine();
    builder.Append(joined);
}
builder.ToString().Dump("C# 5 limitations (alternative)");
