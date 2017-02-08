<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <NuGetReference>Microsoft.ProjectOxford.Vision</NuGetReference>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Media</Namespace>
  <Namespace>System.Windows.Media.Imaging</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>Microsoft.ProjectOxford.Vision</Namespace>
</Query>

var imageUri = new Uri("https://farm1.staticflickr.com/473/19282342454_aa09892839_k_d.jpg", UriKind.Absolute);
var subscriptionKey = "8b8d7104505c40719680e116707db8ec";
var thumbnailSizeData = "1024,300".Split(',');

var width = Convert.ToInt32(thumbnailSizeData.First());
var height = Convert.ToInt32(thumbnailSizeData.Last());

var client = new VisionServiceClient(subscriptionKey);
var imageData = client.GetThumbnailAsync(imageUri.OriginalString, width, height, smartCropping: true).Result;

//reference: https://github.com/Microsoft/Cognitive-Vision-Windows/blob/9fd87bebcd18c3eea39838fa461ab2639a339efa/Sample-WPF/ThumbnailPage.xaml.cs
var bitmap = new BitmapImage();
using (var stream = new MemoryStream(imageData))
{
    stream.Seek(0, SeekOrigin.Begin);
    bitmap.BeginInit();
    bitmap.CacheOption = BitmapCacheOption.OnLoad;
    bitmap.StreamSource = stream;
    bitmap.EndInit();
}

var image = new Image{ Width = bitmap.PixelWidth, Height = bitmap.PixelHeight };
image.Source = bitmap;
image.Dump();