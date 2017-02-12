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

var origin = $"http://placekitten.com/{width}/{height}";
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
    bitmap.DecodePixelHeight = height / 4;
    bitmap.DecodePixelWidth = width / 4;
    bitmap.StreamSource = stream;
    bitmap.EndInit();
}

var image = new Image{ Width = bitmap.DecodePixelWidth, Height = bitmap.DecodePixelHeight };
image.Source = bitmap;
image.Dump($"DecodePixelWidth: {bitmap.DecodePixelWidth}, DecodePixelHeight: {bitmap.DecodePixelHeight}");

/*
    “To save significant application memory, set the DecodePixelWidth or  
     DecodePixelHeight of the BitmapImage value of the image source to the desired 
     height and width of the rendered image. If you don’t do this, the application will 
     cache the image as though it were rendered as its normal size rather [than] just 
     the size that is displayed.”
     
     [https://msdn.microsoft.com/en-us/library/aa970269(v=vs.110).aspx]
*/