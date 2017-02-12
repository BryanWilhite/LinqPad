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
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

int width = 513, height = 512;

var origin = $"http://placekitten.com/{width}/{height}";
var uri = new Uri(origin, UriKind.Absolute);

var bin = Enumerable.Empty<byte>().ToArray();
using (var client = new HttpClient())
{
    bin = client.GetByteArrayAsync(uri).Result;
}

if(!bin.Any()) throw new InvalidOperationException("placekitten returned no data!");

BitmapSource source = null;
using(var stream = new MemoryStream(bin))
{
    var bitmap = new System.Drawing.Bitmap(stream);
    source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
        bitmap.GetHbitmap(),
        IntPtr.Zero,
        Int32Rect.Empty,
        BitmapSizeOptions.FromEmptyOptions());
}

if(source == null) throw new NullReferenceException("The expected bitmap source is not here.");

var image = new Image{ Width = width, Height = height };
image.Source = source;
image.Dump();

/*
    “BitmapSource is the basic building block
    of the Windows Presentation Foundation (WPF) imaging pipeline…”
    It is the base class of BitmapImage.
    [https://msdn.microsoft.com/en-us/library/system.windows.media.imaging.bitmapsource(v=vs.110).aspx]
*/