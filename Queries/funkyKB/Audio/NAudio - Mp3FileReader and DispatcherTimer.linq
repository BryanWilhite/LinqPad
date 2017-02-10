<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Accessibility.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <NuGetReference>MoreLinq</NuGetReference>
  <NuGetReference>NAudio</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>NAudio.Wave</Namespace>
  <Namespace>System.IO</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Windows.Threading</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

async void Main()
{
    var soundRoot = Util.CurrentQuery.GetLinqPadMetaFolder("soundRoot");
    var path = Path.Combine(soundRoot, @"MP3 Collection\Scores\Gabriel Yared - Coco & Igor.mp3");
    var mp3 = new Mp3FileReader(path);
    mp3.WaveFormat.Dump();
    
    var playbackTiming = new DispatcherTimer();
    playbackTiming.Interval = TimeSpan.FromSeconds(1);
    playbackTiming.Tick += (s, args) =>
    {
        var message = string.Format("{0}/{1}", mp3.CurrentTime.TotalSeconds, mp3.TotalTime.TotalSeconds);
        message.Dump();
    };
    
    var output = new WaveOut();
    try
    {
        //mp3.Id3v1Tag.Dump();
        //mp3.Id3v2Tag.Dump();
        
        playbackTiming.Start();
        await output.PlayAsync(mp3 as WaveStream);
    }
    catch(Exception ex)
    {
        ex.Dump();
    }
    finally
    {
        output.Dispose();
        mp3.Dispose();
        playbackTiming.Stop();
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