<Query Kind="Program" />

void Main()
{
    var node = new TreeNode<int>(8)
    {
        Left = new TreeNode<int>(3)
        {
            Left = new TreeNode<int>(1),
            Right = new TreeNode<int>(6)
            {
                Left = new TreeNode<int>(4),
                Right = new TreeNode<int>(7)
            },
        },
        Right = new TreeNode<int>(10)
        {
            Right = new TreeNode<int>(14)
        }
    };

    var data = 4;
    data.Dump(nameof(TreeNodeExtensions.Search));
    node.Search(data);
}

public static class TreeNodeExtensions
{
    public static IEnumerable<TreeNode<T>> GetChildren<T>(this TreeNode<T> node) where T : struct, IComparable
    {
        return (new[] { node.Left, node.Right }).Where(n => n != null);
    }

    public static void ToDisplayText<T>(this TreeNode<T> node, Action<string> displayAction) where T : struct, IComparable
    {
        node.ToDisplayText("  ", false, displayAction);
    }

    public static void ToDisplayText<T>(this TreeNode<T> node, string indent, bool isLast, Action<string> displayAction) where T : struct, IComparable
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

    public static T Search<T>(this TreeNode<T> node, T data) where T : struct, IComparable
    {
        if (node == null) return default;

        node.Data.Dump("searching...");

        if (data.CompareTo(node.Data) == 0) return node.Data;
        if (data.CompareTo(node.Data) < 0) return node.Left.Search(data);
        if (data.CompareTo(node.Data) > 0) return node.Right.Search(data);

        throw new InvalidOperationException();
    }
}

public class TreeNode<T> where T : IComparable
{
    public TreeNode(T data)
    {
        this.Data = data;
    }

    public T Data;

    public TreeNode<T> Left;

    public TreeNode<T> Right;
}

// 📖 https://www.programiz.com/dsa/binary-search-tree
// 📖 https://stackoverflow.com/questions/8642080/icomparable-behaviour-for-null-arguments