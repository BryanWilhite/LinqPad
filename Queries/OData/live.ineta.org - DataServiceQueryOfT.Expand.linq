<Query Kind="Expression">
  <Connection>
    <ID>cf93aefd-6d80-4e18-9a9f-ca5356d175c3</ID>
    <Persist>true</Persist>
    <Driver>AstoriaAuto</Driver>
    <Server>http://live.ineta.org/InetaLiveService.svc/</Server>
  </Connection>
</Query>

LiveAuthors
    .Expand(i => i.LivePresentations)
    .Where (i => i.FirstName == "Scott")

/*
    http://live.ineta.org/InetaLiveService.svc/

    DataServiceQuery<T>.Expand() almost replaces
    IEnumerable<T>.Join() or IQueryable<T>.Join().
*/