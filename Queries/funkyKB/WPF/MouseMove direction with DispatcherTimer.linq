<Query Kind="Statements">
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
</Query>

var xaml = @"
<UserControl
    xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
    <StackPanel>
        <Rectangle
            Name=""RedTangle""
            Fill=""Red""
            Width=""480"" Height=""320""
            />
        <TextBlock
            Name=""MyTextBlock""
            FontSize=""18pt""
            TextAlignment=""Center""
            />
    </StackPanel>
</UserControl>
";

var control = (UserControl)XamlReader.Parse(xaml);
var rectangle = (Rectangle)control.FindName("RedTangle");
var textBlock = (TextBlock)control.FindName("MyTextBlock");
var lastPoint = Mouse.GetPosition(rectangle);
var setPointTimer = new DispatcherTimer(
    TimeSpan.FromSeconds(1),
    DispatcherPriority.Normal,
    (s, args) => {
        var timer = s as DispatcherTimer;
        timer.Stop();
        lastPoint = Mouse.GetPosition(rectangle);
    },
    rectangle.Dispatcher
);

rectangle.MouseMove += (s, args) =>
{
    if(setPointTimer.IsEnabled) return;
    setPointTimer.Start();

    var point = args.GetPosition(rectangle);
    
    var isMovingRight = (point.X > lastPoint.X);
    var isMovingLeft = (point.X < lastPoint.X);
    
    textBlock.Text = "motion unknown";
    if(isMovingRight) textBlock.Text = "moving right";
    if(isMovingLeft) textBlock.Text = "moving left";
};

textBlock.Text = "motion unknown";

control.Dump();