<Query Kind="Statements">
  <Reference Relative="..\..\..\Content\dlls\NAudioExtensions\NAudioExtensions.dll">NAudioExtensions.dll</Reference>
  <NuGetReference>NAudio</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>NAudio.Wave</Namespace>
  <Namespace>Songhay.Extensions</Namespace>
</Query>

var soundRoot = Util.CurrentQuery.GetLinqPadMetaFolder("soundRoot");
var path = Path.Combine(soundRoot, @"ACID Loops\Drums & Percussion\One Shots\Rim Shot 2.wav");
var path2 = Path.Combine(soundRoot, @"ACID Loops\Drums & Percussion\One Shots\Rim Shot 4.wav");

var mStream1 = new MemoryStream(File.ReadAllBytes(path));
var mStream2 = new MemoryStream(File.ReadAllBytes(path2));

try
{
    var streams = new[]
    {
        new WaveFileReader(mStream1),
        new WaveFileReader(mStream2),
    };
    
    streams.ForEach(i =>
    {
        i.WaveFormat.Dump();
        var output = new WaveOut();
        try
        {
            output.PlayAsync(i as WaveStream).Wait();
        }
        catch(Exception ex)
        {
            ex.Dump();
        }
        finally
        {
            output.Dispose();
            i.Dispose(); 
        }
    });
}
catch(Exception ex)
{
    ex.Dump();
}
finally
{
    if(mStream1 != null) mStream1.Dispose();
    if(mStream2 != null) mStream2.Dispose();
}