<Query Kind="Expression">
  <Connection>
    <ID>29f99754-fb24-48f6-8a19-2309f98ab637</ID>
    <Persist>true</Persist>
    <Driver>AstoriaAuto</Driver>
    <Server>http://odata.research.microsoft.com/odata.svc/</Server>
  </Connection>
</Query>

Speakers
    .Expand(i => i.Videos)
    .OrderByDescending(i => i.DateUpdated)
    .Take(10)

/*
    http://odata.research.microsoft.com/odata.svc/
*/