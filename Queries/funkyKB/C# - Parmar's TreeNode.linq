<Query Kind="Program" />

void Main()
{
    var node = new TreeNode<string>("one")
    {
        Left = new TreeNode<string>("two")
        {
            Left = new TreeNode<string>("two-point-five")
        },
        Right = new TreeNode<string>("three")
    };

    node.ToDisplayText(s => s.Dump());
}

public static class TreeNodeExtensions
{
    public static IEnumerable<TreeNode<T>> GetChildren<T>(this TreeNode<T> node)
    {
        return (new[] { node.Left, node.Right }).Where(n => n != null);
    }

    public static void ToDisplayText<T>(this TreeNode<T> node, Action<string> displayAction)
    {
        node.ToDisplayText("  ", false, displayAction);
    }

    public static void ToDisplayText<T>(this TreeNode<T> node, string indent, bool isLast, Action<string> displayAction)
    {
        const string indentLine = "|  ";
        const string indentLastLine = "   ";
        const string nodePrefix = "+- ";

        var displayLine = string.Concat(indent, nodePrefix, node.Data?.ToString());

        displayAction?.Invoke(displayLine);
        indent += isLast ? indentLastLine : indentLine;

        var children = node.GetChildren().ToArray();
        var childMaxIndex = children.Count() - 1;
        var i = 0;

        foreach (var child in children)
        {
            child.ToDisplayText(indent, ++i == childMaxIndex, displayAction);
        }
    }
}

public class TreeNode<T>
{
    public TreeNode(T data)
    {
        this.Data = data;
    }

    public T Data;

    public TreeNode<T> Left;

    public TreeNode<T> Right;
}

// 📖 https://stackoverflow.com/a/8567550/22944
// 📖 https://towardsdatascience.com/4-types-of-tree-traversal-algorithms-d56328450846
