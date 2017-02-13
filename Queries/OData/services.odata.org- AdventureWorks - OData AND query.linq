<Query Kind="Expression">
  <Connection>
    <ID>cd884946-dbf0-42d1-ac90-d3a615f50162</ID>
    <Persist>true</Persist>
    <Driver>AstoriaAuto</Driver>
    <Server>http://services.odata.org/AdventureWorksV3/AdventureWorks.svc</Server>
  </Connection>
</Query>

CompanySales
    .Where(i => i.OrderYear == 2008)
    .Where(i => i.ProductCategory == "Clothing")
/*
    http://services.odata.org/AdventureWorksV3/AdventureWorks.svc
    
*/