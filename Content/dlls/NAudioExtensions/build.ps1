Set-Location $PSScriptRoot

$RuntimeEnvironment = "$env:windir\Microsoft.NET\Framework\v4.0.30319"
$NuGet = "$env:LOCALAPPDATA\LINQPad\NuGet.FW46"
$NuGetNAudio = "$NuGet\NAudio\NAudio.1.7.3\lib\net35"

if(-not(Test-Path $RuntimeEnvironment))
{
    Write-Warning "Cannot find drive/path ${RuntimeEnvironment}:. Exiting script."
    exit
}

& $RuntimeEnvironment\csc `
    /lib:$RuntimeEnvironment `
    /lib:$NuGetNAudio `
    /reference:NAudio.dll `
    /reference:System.Configuration.dll `
    /reference:System.IO.dll `
    /reference:System.Linq.dll `
    /reference:System.Runtime.dll `
    /reference:System.Xml.Linq.dll `
    /target:library `
    /out:NAudioExtensions.dll *.cs