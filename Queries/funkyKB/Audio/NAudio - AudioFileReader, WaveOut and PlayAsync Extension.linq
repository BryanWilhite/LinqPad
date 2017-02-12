<Query Kind="Statements">
  <Connection>
    <ID>ac16a4d3-96f3-48ea-a646-789436ce83cb</ID>
    <Persist>true</Persist>
    <Driver>AstoriaAuto</Driver>
    <Server>http://www.nerddinner.com/Services/OData.svc/</Server>
  </Connection>
  <Reference Relative="..\..\..\Content\dlls\NAudioExtensions\NAudioExtensions.dll">NAudioExtensions.dll</Reference>
  <NuGetReference>NAudio</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>NAudio.Wave</Namespace>
  <Namespace>Songhay.Extensions</Namespace>
</Query>

var soundRoot = Util.CurrentQuery.GetLinqPadMetaFolder("soundRoot");
var path = Path.Combine(soundRoot, @"ACID Loops\Drums & Percussion\One Shots\Rim Shot 2.wav");
var path2 = Path.Combine(soundRoot, @"ACID Loops\Drums & Percussion\One Shots\Rim Shot 4.wav");

var streams = new[]
{
    new AudioFileReader(path),
    new AudioFileReader(path2),
};

streams.ForEach(i =>
{
    i.WaveFormat.Dump();
    var output = new WaveOut();
    try
    {
        output.PlayAsync(i as WaveStream).Wait();
    }
    catch (Exception ex)
    {
        ex.Dump();
    }
    finally
    {
        output.Dispose();
        i.Dispose();
    }
});