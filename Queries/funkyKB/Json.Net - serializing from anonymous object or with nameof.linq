<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

var propertyOne = "One";
var propertyTwo = "Two";
var propertyThree = "Three";

var anon = new
{
    PropertyOne = propertyOne,
    PropertyTwo = propertyTwo,
    PropertyThree = propertyThree
};

var data = JsonConvert.SerializeObject(anon);

data.Dump("JsonConvert.SerializeObject()");

var jO = new JObject
{
    { nameof(propertyOne), propertyOne },
    { nameof(propertyTwo), propertyTwo },
    { nameof(propertyThree), propertyThree },
};

jO.ToString().Dump("JObject.ToString() with nameof");