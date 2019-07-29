<Query Kind="Program">
  <NuGetReference>System.Net.Http</NuGetReference>
  <NuGetReference>YoutubeExtractor</NuGetReference>
  <Namespace>YoutubeExtractor</Namespace>
</Query>

void Main()
{
    IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls("https://www.youtube.com/watch?v=4NNkOK4dOug");
    videoInfos.Dump("video infos");
}