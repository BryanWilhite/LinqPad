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
</Query>

void Main()
{
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
                Name=""StatusH""
                FontSize=""18pt""
                TextAlignment=""Center""
                />
            <TextBlock
                Name=""StatusV""
                FontSize=""18pt""
                TextAlignment=""Center""
                />
            <TextBlock
                Name=""StatusC""
                FontSize=""12pt""
                TextAlignment=""Center""
                />
        </StackPanel>
    </UserControl>
    ";

    var control = (UserControl)XamlReader.Parse(xaml);
    var rectangle = (Rectangle)control.FindName("RedTangle");
    var textC = (TextBlock)control.FindName("StatusC");
    var textH = (TextBlock)control.FindName("StatusH");
    var textV = (TextBlock)control.FindName("StatusV");

    var mouseDirection = new MouseDirection(
        rectangle,
        TimeSpan.FromMilliseconds(120),
        info =>
        {
            var direction = info.MoveDirection;

            if(direction.HasFlag(MouseMoving.Right)) textH.Text = "h: moving right";
            else if(direction.HasFlag(MouseMoving.Left)) textH.Text = "h: moving left";

            if(direction.HasFlag(MouseMoving.Up)) textV.Text = "v: moving up";
            else if(direction.HasFlag(MouseMoving.Down)) textV.Text = "v: moving down";
            
            textC.Text = "count: " + info.MoveCount.ToString("###,###,###");
        });

    textH.Text = "h: motion unknown";
    textV.Text = "v: motion unknown";

    control.Dump();
}

public static class EnumExtensions
{
    public static bool HasFlag(this Enum @enum, Enum enumTest)
    {
        var typeName = @enum.GetType();
        var typeNameTest = enumTest.GetType();

        // see: http://stackoverflow.com/questions/4108828/generic-extension-method-to-see-if-an-enum-contains-a-flag
        if(@enum == null) return false;
        if (!Enum.IsDefined(typeName, enumTest))
        {
            var message = $"Enumeration type mismatch. The flag is of type '{typeNameTest}', was expecting '{typeName}'.";
            throw new ArgumentException(message);
        }

        ulong l = Convert.ToUInt64(enumTest);
        return ((Convert.ToUInt64(@enum) & l) == l);
    }
}

// see: http://geekswithblogs.net/BlackRabbitCoder/archive/2010/07/22/c-fundamentals-combining-enum-values-with-bit-flags.aspx
// see: http://stackoverflow.com/questions/5858439/adding-multiple-values-to-enum-type-variable
enum MouseMoving
{
    Unknown = 0,
    Left = 1,
    Right = 2,
    Up = 4,
    Down = 8
}

class MouseDirectionInfo
{
    public long MoveCount { get; set; }
    
    public MouseMoving MoveDirection { get; set; }

    public MouseEventArgs MouseEventArgs { get; set; }
}

class MouseDirection
{
    public MouseDirection(UIElement input, TimeSpan interval, Action<MouseDirectionInfo> directionAction)
    {
        if(input == null) throw new ArgumentNullException("The expected UI Element is not here.");
        if(directionAction == null) throw new ArgumentNullException("The expected direction action is not here.");

        this._input = input;
        this._directionAction = directionAction;
        this._directionInfo = new MouseDirectionInfo();

        this._lastMousePosition = Mouse.GetPosition(this._input);
        this._mousePositionTimer = new DispatcherTimer(
            interval,
            DispatcherPriority.Normal,
            (s, args) => {
                this._mousePositionTimer.Stop();
                this._lastMousePosition = Mouse.GetPosition(this._input);
            },
            this._input.Dispatcher
        );

        this._input.MouseMove += (s, args) =>
        {
            if(this._mousePositionTimer.IsEnabled) return;
            this._mousePositionTimer.Start();

            var point = args.GetPosition(this._input);

            var isMovingRight = (point.X > this._lastMousePosition.X);
            var isMovingLeft = (point.X < this._lastMousePosition.X);
            var isMovingUp = (point.Y < this._lastMousePosition.Y);
            var isMovingDown = (point.Y > this._lastMousePosition.Y);

            var direction = MouseMoving.Unknown;

            if(isMovingRight) direction = direction | MouseMoving.Right;
            else if(isMovingLeft) direction = direction | MouseMoving.Left;

            if(isMovingUp) direction = direction | MouseMoving.Up;
            else if(isMovingDown) direction = direction | MouseMoving.Down;
            
            this._directionInfo.MoveDirection = direction;
            this._directionInfo.MouseEventArgs = args;

            this._directionInfo.MoveCount++;
            if(this._directionInfo.MoveCount == long.MaxValue)
                this._directionInfo.MoveCount = default(long);

            directionAction.Invoke(this._directionInfo);
        };
    }

    Action<MouseDirectionInfo> _directionAction;
    DispatcherTimer _mousePositionTimer;
    MouseDirectionInfo _directionInfo;
    Point _lastMousePosition;
    UIElement _input;
}