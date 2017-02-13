using MoreLinq;
using Songhay.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml.Linq;

namespace Songhay.Wpf.Extensions
{
    public static class ScrollViewerExtensions
    {
        public static ScrollViewer WithBiggestBoxSvg(this ScrollViewer scrollViewer, IEnumerable<XElement> glyphs)
        {
            if (scrollViewer == null) return null;

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
            return scrollViewer;
        }
    }
}

