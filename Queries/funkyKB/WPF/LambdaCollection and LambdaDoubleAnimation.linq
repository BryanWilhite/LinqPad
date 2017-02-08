<Query Kind="Program">
  <Namespace>System.Collections.ObjectModel</Namespace>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Media.Animation</Namespace>
  <Namespace>System.Windows.Media</Namespace>
  <Namespace>System.Windows.Shapes</Namespace>
</Query>

/*
    “Using Lambdas for WPF or Silverlight Animation”
    by Dmitri Nеstеruk
    [http://www.codeproject.com/Articles/85815/Using-lambdas-for-WPF-or-Silverlight-animation]
*/
void Main()
{
    var canvas = new Canvas
    {
        Background = new SolidColorBrush(Colors.Azure),
        Width = 640, Height = 480
    };

    const int count = 100;

    var circles = new LambdaCollection<Ellipse>(count)
        .WithXY(i => 100.0 + (Math.Pow(i, 1.1) * Math.Sin(i / 4.0 * (Math.PI))),
            i => 100.0 + (Math.Pow(i, 1.1) * Math.Cos(i / 4.0 * (Math.PI))))
        .WithProperty(Ellipse.WidthProperty, i => 1.1 * i)
        .WithProperty(Ellipse.HeightProperty, i => 1.1 * i )
        .WithProperty(Shape.FillProperty, i => new SolidColorBrush(
            Color.FromArgb(255, 0, 0, (byte)(255 - (byte)(12.5 * i)))));
    
    foreach (var circle in circles) canvas.Children.Add(circle);

    var c = new LambdaDoubleAnimationCollection(
        circles.Count,
        i => 100.0 + (Math.Pow(i, 1.1) * Math.Sin(i / 4.0 * (Math.PI))),
        i => 10.0 * i,
        i => new Duration(TimeSpan.FromSeconds(2)),
        i => j => 100.0 / j);

    c.BeginApplyAnimation(circles.Cast<UIElement>().ToArray(), Canvas.LeftProperty);

    canvas.Dump();
}

public class LambdaDoubleAnimation : DoubleAnimation
{
    public Func<double, double> ValueGenerator { get; set; }
    
    protected override double GetCurrentValueCore(double origin, double dst, AnimationClock clock)
    {
        return ValueGenerator(base.GetCurrentValueCore(origin, dst, clock));
    }
}

public class LambdaDoubleAnimationCollection : Collection<LambdaDoubleAnimation>
{
    public LambdaDoubleAnimationCollection(int count, Func<int, double> from, Func<int, double> to,
        Func<int, Duration> duration, Func<int, Func<double, double>> valueGenerator)
    {
        for (int i = 0; i < count; ++i)
        {
            var lda = new LambdaDoubleAnimation
            {
            From = from(i),
            To = to(i),
            Duration = duration(i),
            ValueGenerator = valueGenerator(i)
            };
            Add(lda);
        }
    }
    
    public void BeginApplyAnimation(UIElement[] targets, DependencyProperty property)
    {
        for (int i = 0; i < Count; ++i)
            targets[i].BeginAnimation(property, Items[i]);
    }
}

public class LambdaCollection<T> : Collection<T> where T : DependencyObject, new()
{
    public LambdaCollection(int count) { while (count-- > 0) Add(new T()); }
    
    public LambdaCollection<T> WithProperty<U>(DependencyProperty property, Func<int, U> generator)
    {
        for (int i = 0; i < Count; ++i)
            this[i].SetValue(property, generator(i));
        return this;
    }
    
    public LambdaCollection<T> WithXY<U>(Func<int, U> xGenerator, Func<int, U> yGenerator)
    {
        for (int i = 0; i < Count; ++i)
        {
            this[i].SetValue(Canvas.LeftProperty, xGenerator(i));
            this[i].SetValue(Canvas.TopProperty, yGenerator(i));
        }
        return this;
    }
}
