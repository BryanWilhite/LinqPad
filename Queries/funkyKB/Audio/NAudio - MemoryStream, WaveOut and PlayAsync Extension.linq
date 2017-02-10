<Query Kind="Program">
  <NuGetReference>MoreLinq</NuGetReference>
  <NuGetReference>NAudio</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>NAudio.Wave</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.IO</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
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