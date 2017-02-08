<Query Kind="Program">
  <NuGetReference>MoreLinq</NuGetReference>
  <NuGetReference>NAudio</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>NAudio.Wave</Namespace>
  <Namespace>System.IO</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>NAudio.Wave.SampleProviders</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
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

    var mStream1 = new MemoryStream(File.ReadAllBytes(path));
    var mStream2 = new MemoryStream(File.ReadAllBytes(path2));

    var stream1 = (new WaveFileReader(mStream1)).ToStandardWaveStream();
    var stream2 = (new WaveFileReader(mStream2)).ToStandardWaveStream();
    
    stream1.WaveFormat.Dump();
    stream2.WaveFormat.Dump();
    
    var channel1 = new SampleChannel(stream1);
    var channel2 = new SampleChannel(stream2);
    
    var format = stream1.WaveFormat;
    format = WaveFormat.CreateIeeeFloatWaveFormat(format.SampleRate, format.Channels);
    var mixer = new MixingSampleProvider(format);
    mixer.ReadFully = true;
    
    var output = new WaveOutEvent();
    
    try
    {
        output.PlaybackStopped += (s, args) =>
        {
            output.Dump("PlaybackStopped");
        };
    
        output.Init(mixer);
        output.Play();
    
        stream1.Seek(0, SeekOrigin.Begin);
        mixer.RemoveMixerInput(channel1);
        mixer.RemoveMixerInput(channel1);
        mixer.RemoveMixerInput(channel1);
    
        mixer.AddMixerInput(channel1);
        Task.Delay(stream1.TotalTime).Wait();
    
        mixer.AddMixerInput(channel2);
        Task.Delay(stream2.TotalTime).Wait();
    
        stream1.Seek(0, SeekOrigin.Begin);
        mixer.RemoveMixerInput(channel1);
        mixer.RemoveMixerInput(channel1);
        mixer.RemoveMixerInput(channel1);
    
        mixer.AddMixerInput(channel1);
        Task.Delay(stream1.TotalTime).Wait();
    }
    catch(Exception ex)
    {
        ex.Dump();
    }
    finally
    {
        if(mStream1 != null) mStream1.Dispose();
        if(mStream2 != null) mStream2.Dispose();
    
        if(stream1 != null) stream1.Dispose();
        if(stream2 != null) stream2.Dispose();
    }
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