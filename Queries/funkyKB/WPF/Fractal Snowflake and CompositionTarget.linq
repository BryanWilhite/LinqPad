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

double distanceScale = 1.0 / 3;
double[] dTheta = new double[4] { 0, Math.PI / 3, -2 * Math.PI / 3, Math.PI / 3 };
double snowflakeSize;

int II = 0;
int i = 0;

Canvas canvas = new Canvas
{
    Background = new SolidColorBrush(Colors.Azure),
    Width = 640, Height = 480
};

Polyline pl = new Polyline();
Point snowflakePoint = new Point();

StackPanel panel = new StackPanel
{
    HorizontalAlignment = HorizontalAlignment.Center
};

TextBlock caption = new TextBlock();

void Main()
{
    panel.Children.Add(canvas);
    panel.Children.Add(caption);

    // determine the size of the snowflake:
    double ysize = 0.8 * canvas.Height / (Math.Sqrt(3) * 4 / 3);
    double xsize = 0.8 * canvas.Width / 2;
    double size = 0;
    if (ysize < xsize) size = ysize;
    else size = xsize;

    snowflakeSize = 2 * size;
    pl.Stroke = Brushes.Blue;
    
    canvas.Children.Add(pl);
    
    CompositionTarget.Rendering += DoAnimation;
    
    panel.Dump();
}

void DoAnimation(object sender, EventArgs e)
{
    i += 1;
    if (i % 60 == 0)
    {
        pl.Points.Clear();
        this.DrawSnowFlake(canvas, snowflakeSize, II);
        string str = "Snow Flake - Depth = " +
        II.ToString();
        caption.Text = str;
        II += 1;
        if (II > 5)
        {
            caption.Text = "Snow Flake - Depth = 5. Finished";
            CompositionTarget.Rendering -= DoAnimation;
        }
    }
}

void DrawSnowFlake(Canvas canvas, double length, int depth)
{
    double xmid = canvas.Width / 2;
    double ymid = canvas.Height / 2;
    var pta = new Point[4];
    pta[0] = new Point(xmid, ymid + length / 2 *
    Math.Sqrt(3) * 2 / 3);
    pta[1] = new Point(xmid + length / 2,
    ymid - length / 2 * Math.Sqrt(3) / 3);
    pta[2] = new Point(xmid - length / 2,
    ymid - length / 2 * Math.Sqrt(3) / 3);
    pta[3] = pta[0];
    pl.Points.Add(pta[0]);
    for (int j = 1; j < pta.Length; j++)
    {
        double x1 = pta[j - 1].X;
        double y1 = pta[j - 1].Y;
        double x2 = pta[j].X;
        double y2 = pta[j].Y;
        double dx = x2 - x1;
        double dy = y2 - y1;
        double theta = Math.Atan2(dy, dx);
        this.snowflakePoint = new Point(x1, y1);
        this.RenderEdge(canvas, depth, theta, length);
    }
}

void RenderEdge(Canvas canvas, int depth, double theta, double distance)
{
    var pt = new Point();
    if (depth <= 0)
    {
        pt.X = this.snowflakePoint.X +
        distance * Math.Cos(theta);
        pt.Y = this.snowflakePoint.Y +
        distance * Math.Sin(theta);
        this.pl.Points.Add(pt);
        this.snowflakePoint = pt;
        return;
    }
    distance *= distanceScale;
    for (int j = 0; j < 4; j++)
    {
        theta += dTheta[j];
        this.RenderEdge(canvas, depth - 1, theta, distance);
    }
}