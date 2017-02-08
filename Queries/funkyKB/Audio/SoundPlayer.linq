<Query Kind="Program">
  <NuGetReference>MoreLinq</NuGetReference>
  <NuGetReference>NAudio</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Media</Namespace>
</Query>

string GetSoundRoot()
{
    var linqPadQueryInfo = new DirectoryInfo(Path.GetDirectoryName(Util.CurrentQueryPath));
    var linqPadMetaPath = Path.Combine(linqPadQueryInfo.Parent.Parent.FullName, "LinqPadMeta.json");
    var linqPadMeta = JObject.Parse(File.ReadAllText(linqPadMetaPath));
    var foldersSet = linqPadMeta["LinqPadMeta"]["folders"].ToObject<Dictionary<string, string>>();
    var folderSetKey = string.Format("{0}:{1}", Environment.GetEnvironmentVariable("COMPUTERNAME"), "soundRoot");
    if (!foldersSet.Keys.Contains(folderSetKey)) throw new Exception(string.Format("key {0} is not found; are you on the right device?", folderSetKey));
    
    var soundRoot = foldersSet[folderSetKey];
    return soundRoot;
}

void Main()
{
    var soundRoot = GetSoundRoot();
    var path = Path.Combine(soundRoot, @"ACID Loops\Drums & Percussion\One Shots\Rim Shot 2.wav");
    var path2 = Path.Combine(soundRoot, @"ACID Loops\Drums & Percussion\One Shots\Rim Shot 4.wav");

    var players = new[]
    {
        new SoundPlayer(path),
        new SoundPlayer(path2),
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
