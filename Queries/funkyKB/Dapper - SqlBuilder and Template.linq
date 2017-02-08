<Query Kind="Statements">
  <NuGetReference>Dapper</NuGetReference>
  <NuGetReference>Dapper.SqlBuilder</NuGetReference>
</Query>

// ref: https://liangwu.wordpress.com/2012/08/20/dapper-net-tutorial-ii/

var builder = new Dapper.SqlBuilder();
var template = builder.AddTemplate("SELECT TOP 10 /**select**/ FROM Customers /**where**/ ");
builder.Select("*");

var test1 = false;
var test2 = true;
var test3 = true;

if (test1) builder.Where("CompanyName like 'International%'");
if (test2) builder.Where("City like 'Los%'");
if (test3) builder.Where("Country = 'United States of America'");
builder.Where("InceptDate <= getDate()");
template.RawSql.Dump("Dynamic WHERE");

builder = new Dapper.SqlBuilder();
template = builder.AddTemplate("SELECT /**select**/ FROM Orders /**groupby**/ /**having**/ /**orderby**/");
builder
    .Select("count(*) AS Count, ShipCity")
    .GroupBy("ShipCity")
    .Having("ShipCity <> 'Amsterdam'")
    .OrderBy("count(*) DESC")
    ;
template.RawSql.Dump("GroupBy(), Having() and OrderBy()");

builder = new Dapper.SqlBuilder();
template = builder.AddTemplate("SELECT /**select**/ FROM Orders /**innerjoin**/ /**where**/");
builder
    .Select("Orders.*")
    .InnerJoin("Customers ON Orders.CustomerID = Customers.CustomerID")
    .Where("Customers.City = 'London'")
    ;
template.RawSql.Dump("InnerJoin()");

builder = new Dapper.SqlBuilder();
template = builder.AddTemplate("SELECT /**select**/ FROM Orders /**where**/");
builder
    .Select("*")
    .OrWhere("((CustomerID = 'VINET') AND (OrderDate < '2005-01-01'))")
    .OrWhere("((CustomerID = 'TOMSP') AND (OrderDate < '2004-06-30'))")
    ;
template.RawSql.Dump("nested WHERE clauses");