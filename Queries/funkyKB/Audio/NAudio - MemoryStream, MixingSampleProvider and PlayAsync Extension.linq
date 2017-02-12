<Query Kind="Statements">
  <Reference Relative="..\..\..\Content\dlls\NAudioExtensions\NAudioExtensions.dll">NAudioExtensions.dll</Reference>
  <NuGetReference>NAudio</NuGetReference>
  <Namespace>NAudio.Wave</Namespace>
  <Namespace>Songhay.Extensions</Namespace>
  <Namespace>NAudio.Wave.SampleProviders</Namespace>
</Query>

var soundRoot = Util.CurrentQuery.GetLinqPadMetaFolder("soundRoot");
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