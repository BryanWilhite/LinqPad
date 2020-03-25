<Query Kind="Program" />

void Main()
{
    var array = new[] { 4, 10, 3, 5, 1, 2 }; // 💡 derive a max/min heap by just sorting `array`

    var node = new TreeNode<string>();

    node.Set(array);

    node?.ToDisplayText(s => s.Dump());
}

public static class TreeNodeExtensions
{
    public static void Set<TArray>(this TreeNode<string> node, TArray[] array)
    {
        node.Set(array, 0);
    }

    public static void Set<TArray>(this TreeNode<string> node, TArray[] array, int arrayIndex)
    {
        var indexOfParent = (arrayIndex - 1) / 2;
        var indexLeftChild = (2 * arrayIndex + 1);
        var indexRightChild = (2 * arrayIndex + 2);

        var msg = $"n: {arrayIndex} => p:{indexOfParent}, l:{indexLeftChild}, r:{indexRightChild}";

        if (string.IsNullOrWhiteSpace(node.Data))
            node.Data = $"node {array[indexOfParent]} [{msg}]";

        if (indexLeftChild < array.Length)
        {
            node.Left = new TreeNode<string>($"node {array[indexLeftChild]} [{msg}]");
            node.Left.Set(array, ++arrayIndex);
        }

        if (indexRightChild < array.Length)
        {
            node.Right = new TreeNode<string>($"node {array[indexRightChild]} [{msg}]");
            node.Right.Set(array, ++arrayIndex);
        }
    }

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
    public TreeNode() {}

    public TreeNode(T data)
    {
        this.Data = data;
    }

    public T Data;

    public TreeNode<T> Left;

    public TreeNode<T> Right;
}

// 📖 https://stackoverflow.com/a/8567550/22944
// 📖 https://www.geeksforgeeks.org/building-heap-from-array/
// 📖 https://towardsdatascience.com/4-types-of-tree-traversal-algorithms-d56328450846
