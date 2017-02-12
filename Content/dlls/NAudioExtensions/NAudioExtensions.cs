using NAudio.Wave;
using System;
using System.IO;
using System.Threading.Tasks;
using NAudio.Wave.SampleProviders;

namespace Songhay.Extensions
{
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
}