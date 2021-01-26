<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

dynamic dy = JObject.Parse(@"{ ""myThing"": 42 }");

bool test = dy.myThing == 42;

dy.myOtherThing = "forty-three";

test.Dump(nameof(test));

JObject jO = JObject.FromObject(dy); //using `var` here will make `jO` Dynamic

jO.ToString().Dump(nameof(jO));

var propertyName = nameof(dy.myThing);

test = jO.Property(propertyName) != null;

test.Dump(nameof(test));
