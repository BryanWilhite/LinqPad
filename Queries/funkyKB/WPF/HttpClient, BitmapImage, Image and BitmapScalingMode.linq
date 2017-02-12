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

int width = 65, height = 64;

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
    bitmap.StreamSource = stream;
    bitmap.EndInit();
}

var imageFastest = new Image{ Width = bitmap.PixelWidth * 4, Height = bitmap.PixelHeight * 4 };
RenderOptions.SetBitmapScalingMode(imageFastest, BitmapScalingMode.NearestNeighbor);
imageFastest.Source = bitmap;
imageFastest.Dump("BitmapScalingMode.NearestNeighbor");

var image = new Image{ Width = bitmap.PixelWidth * 4, Height = bitmap.PixelHeight * 4 };
RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.Linear);
image.Source = bitmap;
image.Dump("BitmapScalingMode.Linear == BitmapScalingMode.LowQuality");

var imageSlowest = new Image{ Width = bitmap.PixelWidth * 4, Height = bitmap.PixelHeight * 4 };
RenderOptions.SetBitmapScalingMode(imageSlowest, BitmapScalingMode.Fant);
imageSlowest.Source = bitmap;
imageSlowest.Dump("BitmapScalingMode.Fant == BitmapScalingMode.HighQuality");