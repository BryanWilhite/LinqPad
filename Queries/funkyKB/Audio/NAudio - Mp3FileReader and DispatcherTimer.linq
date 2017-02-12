<Query Kind="Program">
  <Reference Relative="..\..\..\Content\dlls\NAudioExtensions\NAudioExtensions.dll">NAudioExtensions.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Accessibility.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <NuGetReference>NAudio</NuGetReference>
  <Namespace>NAudio.Wave</Namespace>
  <Namespace>Songhay.Extensions</Namespace>
  <Namespace>System.Windows.Threading</Namespace>
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
        var message = $"{mp3.CurrentTime.TotalSeconds}/{mp3.TotalTime.TotalSeconds}";
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