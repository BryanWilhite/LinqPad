<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\Accessibility.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\PresentationUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\ReachFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Deployment.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\System.Printing.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\UIAutomationProvider.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\UIAutomationTypes.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\WindowsBase.dll</Reference>
  <NuGetReference>MoreLinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Drawing.Text</Namespace>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Documents</Namespace>
  <Namespace>System.Windows.Markup</Namespace>
  <Namespace>System.Windows.Media</Namespace>
</Query>

void Main()
{
    var glyphs = Util.CurrentQuery.LoadBiggestBoxSvgGlyphs("mfglabsiconset-webfont");
    (new ScrollViewer()).LoadBiggestBoxSvg(glyphs);
}

public static class ScrollViewerExtensions
{
    public static void LoadBiggestBoxSvg(this ScrollViewer scrollViewer, IEnumerable<XElement> glyphs)
    {
        if (scrollViewer == null) return;

        scrollViewer.Background = new SolidColorBrush(Colors.Azure);
        scrollViewer.Padding = new Thickness(4);
        scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
        scrollViewer.Width = 960;
        scrollViewer.Height = 600;
    
        var wrapPanel = new WrapPanel { Orientation = Orientation.Horizontal };
    
        scrollViewer.Content = wrapPanel;
    
        glyphs.ForEach(i =>
        {
            var d = i.ToAttributeValueOrDefault("d", string.Empty);
            var unicode = i.ToAttributeValueOrDefault("unicode", string.Empty);
            var xaml = @"
                <Grid xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                    Margin=""4"">
                    <Grid.RowDefinitions>
                        <RowDefinition />
                        <RowDefinition />
                    </Grid.RowDefinitions>
                    <Border
                        BorderThickness=""2"" BorderBrush=""DarkGray""
                        MinHeight=""96"" Padding=""4""
                        Width=""96"" Height=""Auto"">
                        <Path
                            Data=""{Binding StreamGeometry, Mode=OneWay}""
                            Fill=""Black""
                            Stretch=""Uniform"">
                            <Path.LayoutTransform>
                                <ScaleTransform ScaleY=""-1"" />
                            </Path.LayoutTransform>
                        </Path>
                    </Border>
                    <TextBlock Grid.Row=""1""
                        Margin=""0 0 0 24""
                        TextAlignment=""Center"">
                        <Hyperlink Name=""GlyphCommand"">
                            <Run Text=""{Binding EntityDisplayText, Mode=OneWay}"" />
                        </Hyperlink>
                    </TextBlock>
                </Grid>
                ";
            var control = (Grid)XamlReader.Parse(xaml);
            control.DataContext = new
            {
                StreamGeometry = d,
                EntityDisplayText = unicode
            };
    
            var link = (Hyperlink)control.FindName("GlyphCommand");
            link.Click += (s, args) =>
            {
                var data = string.Format("http://fontawesome.io/icons/{0}unicode: {1}{0}data: {2}", Environment.NewLine, unicode, d);
                Clipboard.SetText(data);
            };
    
            wrapPanel.Children.Add(control);
        });
    
        scrollViewer.UpdateLayout();
        scrollViewer.Dump();
    }
}
