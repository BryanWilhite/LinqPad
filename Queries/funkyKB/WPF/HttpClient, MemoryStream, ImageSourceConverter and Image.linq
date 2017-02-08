<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Media</Namespace>
  <Namespace>System.Windows.Media.Imaging</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

int width = 513, height = 512;

var origin = string.Format("http://placekitten.com/{0}/{1}", width, height);
var uri = new Uri(origin, UriKind.Absolute);

var bin = Enumerable.Empty<byte>().ToArray();
using (var client = new HttpClient())
{
    bin = client.GetByteArrayAsync(uri).Result;
}

if (!bin.Any()) throw new InvalidOperationException("placekitten returned no data!");

ImageSource source = null;
using (var stream = new MemoryStream(bin))
{
    var converter = new ImageSourceConverter();
    source = (ImageSource)converter.ConvertFrom(stream.ToArray());
    /*
        stream.ToArray() makes a copy, allowing the stream to be disposed
        such that image can be displayed.
    */
}

if(source == null) throw new NullReferenceException("The expected source is not here.");

var image = new Image{ Width = width, Height = height };
image.Source = source;
image.Dump();