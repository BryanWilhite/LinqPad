<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\WindowsBase.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\UIAutomationProvider.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Deployment.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\PresentationUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\System.Printing.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\ReachFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\UIAutomationTypes.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Accessibility.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Markup</Namespace>
  <Namespace>System.Drawing.Text</Namespace>
  <Namespace>System.Windows.Media</Namespace>
  <Namespace>System.Windows</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

void Main()
{
    var osData = new InstalledFontCollection();
    var fontFamily = osData.Families.FirstOrDefault(i => i.Name == "FontAwesome");
    if (fontFamily == null) throw new SystemException("The expected System Font is not here.");

    var path = Path.Combine(Util.CurrentQuery.GetLinqPadMetaFolder("visionRoot"),
        @"webfonts\font-awesome-4.2.0\fonts\fontawesome-webfont.svg");
    //You cannot stop .NET from condensing entities so load as string:
    var xml = File.ReadAllText(path).Replace("unicode=\"&#", "unicode=\"&amp;#");
    var svgFont = XDocument.Parse(xml);

    var scrollViewer = new ScrollViewer
    {
        Background = new SolidColorBrush(Colors.Azure),
        Padding = new Thickness(4),
        VerticalScrollBarVisibility = ScrollBarVisibility.Visible,
        Width = 960,
        Height = 600
    };
    var wrapPanel = new WrapPanel { Orientation = Orientation.Horizontal };

    scrollViewer.Content = wrapPanel;

    XNamespace svg = "http://www.w3.org/2000/svg";
    svgFont.Descendants(svg + "glyph")
        .ToList().ForEach(i =>
        {
            var unicode = i.Attribute("unicode").Value;
            var xaml = @"
            <Grid xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                Margin=""4"">
                <Grid.Resources>
                    <Style TargetType=""TextBox"">
                        <Setter Property=""Background"" Value=""Transparent"" />
                        <Setter Property=""HorizontalContentAlignment"" Value=""Center"" />
                    </Style>
                </Grid.Resources>
                <Grid.RowDefinitions>
                    <RowDefinition />
                    <RowDefinition />
                </Grid.RowDefinitions>
                <TextBox Grid.Row=""0""
                    FontFamily=""{Binding FontFamily, Mode=OneWay}""
                    FontSize=""24""
                    Margin=""2"" Padding=""2""
                    Text=""{Binding Glyph, Mode=OneWay}""
                    />
                <TextBox Grid.Row=""1""
                    BorderThickness=""0""
                    Text=""{Binding GlyphDisplayText, Mode=OneWay}""
                    />
            </Grid>
            ";
            var control = (Grid)XamlReader.Parse(xaml);
            control.DataContext = new
            {
                FontFamily = "FontAwesome",
                Glyph = System.Net.WebUtility.HtmlDecode(unicode),
                GlyphDisplayText = unicode
            };
            wrapPanel.Children.Add(control);
        });

    scrollViewer.Dump();
}