<Query Kind="Program">
  <Connection>
    <ID>ac16a4d3-96f3-48ea-a646-789436ce83cb</ID>
    <Persist>true</Persist>
    <Driver>AstoriaAuto</Driver>
    <Server>http://www.nerddinner.com/Services/OData.svc/</Server>
  </Connection>
  <NuGetReference>MoreLinq</NuGetReference>
  <NuGetReference>NAudio</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>NAudio.Wave</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.IO</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
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
}

public static class NAudioExtensions
{
    public static Task<WaveOut> PlayAsync(this WaveOut output, IWaveProvider provider)
    {
        if (output == null) throw new ArgumentNullException("The expected sound output is not here.");

        var completionSource = new TaskCompletionSource<WaveOut>();
        output.PlaybackStopped += (s, args) =>
        {
            if (args.Exception != null) completionSource.SetException(args.Exception);
            else completionSource.SetResult(output);
        };

        output.Init(provider);
        output.Play();

        return completionSource.Task;
    }

    public static WaveStream ToStandardWaveStream(this WaveStream stream)
    {
        if (stream == null) throw new ArgumentNullException("The expected Wave Stream is not here.");

        var encoding = stream.WaveFormat.Encoding;
        var isNotPcmFormat = (encoding != WaveFormatEncoding.Pcm);
        var isNotIeeeFloatFormat = (encoding != WaveFormatEncoding.IeeeFloat);

        Func<WaveStream> convert = () =>
        {
            stream = WaveFormatConversionStream.CreatePcmStream(stream);
            stream = new BlockAlignReductionStream(stream);
            return stream;
        };

        return (isNotPcmFormat && isNotIeeeFloatFormat) ? convert() : stream;
    }
}