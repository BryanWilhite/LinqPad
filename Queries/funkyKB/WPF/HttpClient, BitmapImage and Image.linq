<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Media</Namespace>
  <Namespace>System.Windows.Media.Imaging</Namespace>
  <Namespace>System.Net</Namespace>
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

var bitmap = new BitmapImage();
using (var stream = new MemoryStream(bin))
{
    bitmap.BeginInit();
    bitmap.CacheOption = BitmapCacheOption.OnLoad;
    /*
        “This line…is very important…the Image will be loaded into memory,
        and each request for the Image will be fetched from that memory store.
        So after closing or disposing the memory stream,
        the Image will be available in the memory cache.”
        —Tarun Kumar Singh
        [https://tarundotnet.wordpress.com/2011/12/25/how-to-convert-bytes-to-bitmapimage-in-wpf/]
    */
    bitmap.StreamSource = stream;
    bitmap.EndInit();
}

var image = new Image{ Width = bitmap.PixelWidth, Height = bitmap.PixelHeight };
image.Source = bitmap;
image.Dump();