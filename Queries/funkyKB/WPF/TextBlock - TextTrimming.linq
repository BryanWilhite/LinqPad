<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Markup</Namespace>
</Query>

var xaml = @"
<UserControl
    xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
    xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
    xmlns:sys=""clr-namespace:System;assembly=mscorlib"">
    <UserControl.Resources>
        <Style TargetType=""{x:Type TextBlock}"">
            <Setter Property=""Background"" Value=""LightGoldenrodYellow"" />
            <Setter Property=""FontSize"" Value=""14pt"" />
            <Setter Property=""Margin"" Value=""0 16 0 0"" />
            <Setter Property=""TextWrapping"" Value=""NoWrap"" />
            <Setter Property=""Width"" Value=""120"" />
        </Style>
    </UserControl.Resources>
    <StackPanel>
        <TextBlock TextTrimming=""CharacterEllipsis"">
One<LineBreak/>
two two<LineBreak/>
Three Three Three<LineBreak/>
four four four four<LineBreak/>
Five Five Five Five Five<LineBreak/>
six six six six six six<LineBreak/>
Seven Seven Seven Seven Seven Seven Seven
        </TextBlock>
        <TextBlock TextTrimming=""None"">
One<LineBreak/>
two two<LineBreak/>
Three Three Three<LineBreak/>
four four four four<LineBreak/>
Five Five Five Five Five<LineBreak/>
six six six six six six<LineBreak/>
Seven Seven Seven Seven Seven Seven Seven
        </TextBlock>
        <TextBlock TextTrimming=""WordEllipsis"">
One<LineBreak/>
two two<LineBreak/>
Three Three Three<LineBreak/>
four four four four<LineBreak/>
Five Five Five Five Five<LineBreak/>
six six six six six six<LineBreak/>
Seven Seven Seven Seven Seven Seven Seven
        </TextBlock>
    </StackPanel>
</UserControl>
";

var view = (UserControl)XamlReader.Parse(xaml);
view.Dump();