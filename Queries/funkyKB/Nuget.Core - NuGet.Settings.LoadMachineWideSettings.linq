<Query Kind="Statements">
  <NuGetReference>NuGet.Core</NuGetReference>
  <Namespace>NuGet</Namespace>
</Query>

/*
    https://github.com/scriptcs/scriptcs/blob/ffcd2ffc51c0e531fbdd06a43cb36c788c582698/src/ScriptCs.Hosting/Package/NugetMachineWideSettings.cs

    http://lastexitcode.com/projects/NuGet/FileLocations/

    https://stackoverflow.com/questions/39569318/how-to-download-and-unzip-packages-using-nuget-v3-api
*/

var baseDirectory = Environment
    .GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

var nugetFileSystem = new PhysicalFileSystem(baseDirectory);

var settings = NuGet.Settings
    .LoadMachineWideSettings(nugetFileSystem);

settings.Dump();

baseDirectory = Environment
    .GetFolderPath(Environment.SpecialFolder.ApplicationData);

nugetFileSystem = new PhysicalFileSystem(baseDirectory);

var localSettings = NuGet.Settings
    .LoadDefaultSettings(nugetFileSystem, @"NuGet\NuGet.Config", null);

localSettings.Dump();