Set-Location $PSScriptRoot

$RuntimeEnvironment = "$env:windir\Microsoft.NET\Framework\v4.0.30319"
$NuGet = "$env:LOCALAPPDATA\LINQPad\NuGet.FW46"
$NuGetMoreLinq = "$NuGet\MoreLinq\morelinq.2.1.0\lib\netstandard1.0"
$NuGetSonghayCore = "$NuGet\SonghayCore\SonghayCore.1.6.1\lib\net452"

if(-not(Test-Path $RuntimeEnvironment))
{
    Write-Warning "Cannot find drive/path ${RuntimeEnvironment}:. Exiting script."
    exit
}

& $RuntimeEnvironment\csc `
    /lib:$RuntimeEnvironment `
    /lib:$RuntimeEnvironment\WPF `
    /lib:$NuGetMoreLinq `
    /lib:$NuGetSonghayCore `
    /reference:Accessibility.dll `
    /reference:MoreLinq.dll `
    /reference:PresentationCore.dll `
    /reference:PresentationFramework.dll `
    /reference:PresentationUI.dll `
    /reference:ReachFramework.dll `
    /reference:SonghayCore.dll `
    /reference:System.Configuration.dll `
    /reference:System.IO.dll `
    /reference:System.Linq.dll `
    /reference:System.Deployment.dll `
    /reference:System.Printing.dll `
    /reference:System.Runtime.dll `
    /reference:System.Xaml.dll `
    /reference:System.Xml.Linq.dll `
    /reference:UIAutomationProvider.dll `
    /reference:UIAutomationTypes.dll `
    /reference:WindowsBase.dll `
    /target:library `
    /out:WpfExtensions.dll *.cs