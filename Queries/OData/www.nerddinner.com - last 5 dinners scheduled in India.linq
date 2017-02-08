<Query Kind="Expression">
  <Connection>
    <ID>ac16a4d3-96f3-48ea-a646-789436ce83cb</ID>
    <Persist>true</Persist>
    <Driver>AstoriaAuto</Driver>
    <Server>http://www.nerddinner.com/Services/OData.svc/</Server>
  </Connection>
</Query>

Dinners
    .Where(i => i.Country == "India")
    .OrderByDescending(i => i.EventDate)
    .Take(5)

/*
    http://www.nerddinner.com/Services/OData.svc/
*/