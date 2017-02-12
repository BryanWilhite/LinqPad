<Query Kind="Statements">
  <NuGetReference>MoreLinq</NuGetReference>
  <NuGetReference>NAudio</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Media</Namespace>
</Query>

var soundRoot = Util.CurrentQuery.GetLinqPadMetaFolder("soundRoot");
var path = Path.Combine(soundRoot, @"ACID Loops\Drums & Percussion\One Shots\Rim Shot 2.wav");
var path2 = Path.Combine(soundRoot, @"ACID Loops\Drums & Percussion\One Shots\Rim Shot 4.wav");

var mStream1 = new MemoryStream(File.ReadAllBytes(path));
var mStream2 = new MemoryStream(File.ReadAllBytes(path2));

try
{
    var players = new[]
    {
        new SoundPlayer(mStream1),
        new SoundPlayer(mStream2),
    };

    players.ForEach(i =>
    {
        i.Dump();
        try
        {
            i.PlaySync();
        }
        catch(Exception ex)
        {
            ex.Dump();
        }
        finally
        {
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