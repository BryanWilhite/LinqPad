<Query Kind="Expression">
  <Connection>
    <ID>02b5c428-4bac-46c9-968b-43cb2214a705</ID>
    <Persist>true</Persist>
    <Driver>AstoriaAuto</Driver>
    <Server>http://packages.nuget.org/v1/FeedService.svc/</Server>
  </Connection>
</Query>

Packages
    .Where(i => i.Title.Contains("Rx"))

/*
    http://packages.nuget.org/v1/FeedService.svc/
*/