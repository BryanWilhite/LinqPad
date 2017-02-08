<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\UIAutomationProvider.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\UIAutomationTypes.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\ReachFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\System.Printing.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Accessibility.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Deployment.dll</Reference>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Media</Namespace>
  <Namespace>System.Windows.Shapes</Namespace>
  <Namespace>System.Windows</Namespace>
</Query>

/*
    “Drawing Fractals via WPF”
    [http://www.codeproject.com/Articles/124906/Drawing-Fractals-via-WPF]
*/

double lengthScale = 0.75;
double deltaTheta = Math.PI / 5;

int II = 1;
int i = 0;

Canvas canvas = new Canvas
{
    Background = new SolidColorBrush(Colors.Azure),
    Width = 640, Height = 480
};

StackPanel panel = new StackPanel
{
    HorizontalAlignment = HorizontalAlignment.Center
};

TextBlock caption = new TextBlock();

void Main()
{
    panel.Children.Add(canvas);
    panel.Children.Add(caption);

    CompositionTarget.Rendering += DoAnimation;
    
    panel.Dump();
}

void DoAnimation(object sender, EventArgs e)
{
    i += 1;
    if (i % 60 == 0)
    {
        var point = new Point(canvas.Width / 2, 0.93 * canvas.Height);
        this.DrawBinaryTree(canvas, II, point, 0.2 * canvas.Width, -Math.PI / 2);
        caption.Text = "Binary Tree - Depth = " + II.ToString();

        II += 1;
        if (II > 10)
        {
            caption.Text = "Binary Tree - Depth = 10. Finished";
            CompositionTarget.Rendering -= DoAnimation;
        }
    }
}

void DrawBinaryTree(Canvas canvas, int depth, Point pt, double length, double theta)
{
    double x1 = pt.X + length * Math.Cos(theta);
    double y1 = pt.Y + length * Math.Sin(theta);
    Line line = new Line();
    line.Stroke = Brushes.Blue;
    line.X1 = pt.X;
    line.Y1 = pt.Y;
    line.X2 = x1;
    line.Y2 = y1;
    canvas.Children.Add(line);

    if (depth > 1)
    {
        DrawBinaryTree(canvas, depth - 1,
        new Point(x1, y1),
        length * lengthScale, theta + deltaTheta);
        DrawBinaryTree(canvas, depth - 1,
        new Point(x1, y1),
        length * lengthScale, theta - deltaTheta);
    }
    else return;
}