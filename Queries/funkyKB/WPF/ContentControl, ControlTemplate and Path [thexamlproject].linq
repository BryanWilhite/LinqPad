<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <NuGetReference>Prism.WPF</NuGetReference>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Markup</Namespace>
  <Namespace>Prism.Mvvm</Namespace>
</Query>

var xaml = @"
<UserControl
    xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
    xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
    xmlns:sys=""clr-namespace:System;assembly=mscorlib"">
    <UserControl.Resources>
        <Style TargetType=""{x:Type TextBlock}"">
            <Setter Property=""Margin"" Value=""8"" />
            <Setter Property=""TextWrapping"" Value=""Wrap"" />
        </Style>
        <Style x:Key=""SymbolWarningPathStyle"" TargetType=""{x:Type Path}"">
            <!--http://www.thexamlproject.com/#/artwork/491-->
            <!--Warning:-->
            <Setter Property=""Data"" Value=""F1 M 34.7541,26.4939L 20.5932,1.72809C 19.9132,0.624023 18.9211,0.0480042 17.6171,0C 16.265,0.0480042 15.2729,0.624023 14.6409,1.72809L 0.480042,26.4939C 0.151978,27.0559 -0.00799561,27.6424 0,28.2534C 0.0289917,29.2073 0.378998,29.9982 1.05005,30.6259C 1.72107,31.2536 2.53915,31.579 3.50421,31.6022L 31.7299,31.6022C 32.693,31.5848 33.503,31.271 34.1601,30.6607C 34.8171,30.0504 35.1591,29.248 35.1861,28.2534C 35.1861,27.6424 35.0421,27.0559 34.7541,26.4939 Z M 15.0729,8.06448L 20.2092,8.06448L 20.2092,19.7072L 15.0729,19.7072L 15.0729,8.06448 Z M 17.665,22.4372C 18.4991,22.4576 19.1832,22.7468 19.7172,23.3048C 20.2512,23.8628 20.5272,24.5674 20.5453,25.4186C 20.5272,26.2444 20.2512,26.9266 19.7172,27.4652C 19.1832,28.0039 18.4991,28.2829 17.665,28.3022C 16.831,28.2829 16.147,28.0039 15.6129,27.4653C 15.0789,26.9266 14.8029,26.2444 14.7849,25.4186C 14.8029,24.546 15.0789,23.8353 15.6129,23.2864C 16.147,22.7376 16.831,22.4545 17.665,22.4372 Z "" />
            <Setter Property=""Fill"" Value=""Black"" />
            <Setter Property=""Stretch"" Value=""UniformToFill"" />
        </Style>
        <ControlTemplate x:Key=""SymbolWarningTemplate"" TargetType=""{x:Type ContentControl}"">
            <Viewbox Width=""16"" Height=""16"">
                <Path Fill=""Red"" Style=""{StaticResource SymbolWarningPathStyle}"" />
            </Viewbox>
        </ControlTemplate>
    </UserControl.Resources>
    <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition />
            <ColumnDefinition />
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition />
            <RowDefinition />
        </Grid.RowDefinitions>
        <ContentControl Template=""{StaticResource SymbolWarningTemplate}"" />
        <TextBlock Grid.Column=""1"">
            <Run Text=""Scale down a Path with"" />
            <Run FontFamily=""Consolas"" Text=""Viewbox"" />
            <Run Text=""."" />
        </TextBlock>

        <Path Grid.Row=""1"" Style=""{StaticResource SymbolWarningPathStyle}"" />
        <TextBlock Grid.Column=""1""  Grid.Row=""1"">
            <Run Text=""Scale up a Path with"" />
            <Run FontFamily=""Consolas"" Text=""UniformToFill"" />
            <Run Text=""stretch in Panel like Grid."" />
        </TextBlock>
    </Grid>
</UserControl>
";

var view = (UserControl)XamlReader.Parse(xaml);
view.Dump();