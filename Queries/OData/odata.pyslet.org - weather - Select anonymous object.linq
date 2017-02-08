<Query Kind="Expression">
  <Connection>
    <ID>4e5b931a-93ee-4f5d-9ff4-1040fc1d3045</ID>
    <Persist>true</Persist>
    <Driver>AstoriaAuto</Driver>
    <Server>http://odata.pyslet.org/weather/</Server>
  </Connection>
</Query>

DataPoints
    .Where(i => i.TimePoint.Year == DateTime.Now.Year)
    .Where(i => i.TimePoint.Month == DateTime.Now.Month)
    .Select (i =>
        new
        {
            DewPoint = i.DewPoint,
            Humidity = i.Humidity,
            Pressure = i.Pressure,
            Rain = i.Rain,
            Sun = i.Sun,
            Temperature = i.Temperature,
            TimePoint = i.TimePoint,
            WindDirection= i.WindDirection,
            WindSpeed = i.WindSpeed,
            WindSpeedMax = i.WindSpeedMax
        })
    .ToList()
    .OrderByDescending(i => i.TimePoint)
/*
    http://odata.pyslet.org/weather/

    must Select anonymous object because
    DataPoints.SunRainStart throws an error
    (`The string '00:00:00' is not a valid TimeSpan value.`)
*/