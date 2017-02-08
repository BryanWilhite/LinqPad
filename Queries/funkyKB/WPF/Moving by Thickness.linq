<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Markup</Namespace>
  <Namespace>System.Windows.Shapes</Namespace>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Threading</Namespace>
  <Namespace>System.Windows.Input</Namespace>
  <Namespace>System.Windows.Media.Animation</Namespace>
</Query>

void Main()
{
    var xaml = @"
    <UserControl
        xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
        <Border BorderBrush=""Black"" BorderThickness=""4"" Width=""640"" Height=""480"">
            <Grid>
                <Rectangle
                    Name=""RedTangle""
                    Fill=""Red"" Margin=""0 160 0 0""
                    Width=""480"" Height=""320""
                    />
                <Grid>
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition />
                        <ColumnDefinition />
                    </Grid.ColumnDefinitions>
                    <Button
                        Name=""MoveUpCommand""
                        Content=""Move Up""
                        Width=""96"" Height=""48""
                        />
                    <Button Grid.Column=""1""
                        Name=""MoveDownCommand""
                        Content=""Move Down""
                        Width=""96"" Height=""48""
                        />
                </Grid>
            </Grid>
        </Border>
    </UserControl>
    ";

    var tangleTop = 160d;
    var delta = 10d;
    var top = tangleTop;
    var control = (UserControl)XamlReader.Parse(xaml);
    var rectangle = (Rectangle)control.FindName("RedTangle");
    var moveUpCommand = (Button)control.FindName("MoveUpCommand");
    var moveDownCommand = (Button)control.FindName("MoveDownCommand");

    moveUpCommand.Click += (s, args) =>
    {
        top -= delta;
        if(top < -tangleTop) top = -tangleTop;
        rectangle.Margin = new Thickness(0, top, 0, 0);
    };

    moveDownCommand.Click += (s, args) =>
    {
        top += delta;
        if(top > tangleTop) top = tangleTop;
        rectangle.Margin = new Thickness(0, top, 0, 0);
    };

    control.Dump();
}