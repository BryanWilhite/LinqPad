<Query Kind="Expression">
  <Connection>
    <ID>23edbc8d-1783-4bb0-9ad4-9646670cd3df</ID>
    <Persist>true</Persist>
    <Driver>AstoriaAuto</Driver>
    <Server>http://tv.telerik.com/services/odata.svc/</Server>
  </Connection>
</Query>

Authors
    .Expand(i => i.Videos)
    .Take(10)

/*
    http://tv.telerik.com/services/odata.svc/
*/