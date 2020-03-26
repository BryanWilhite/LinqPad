<Query Kind="Program" />

void Main()
{
    var node = new TreeNode<int>(1)
    {
        Left = new TreeNode<int>(12)
        {
            Left = new TreeNode<int>(5),
            Right = new TreeNode<int>(6),
        },
        Right = new TreeNode<int>(9)
    };

    var s = string.Empty;
    node.TraverseInOrder(n => s += (string.IsNullOrWhiteSpace(s)) ? $"{n.Data}" : $" -> {n.Data}");
    s.Dump(nameof(TreeNodeExtensions.TraverseInOrder));

    s = string.Empty;
    node.TraverseLevelOrder(n => s += (string.IsNullOrWhiteSpace(s)) ? $"{n.Data}" : $" -> {n.Data}");
    s.Dump(nameof(TreeNodeExtensions.TraverseLevelOrder));

    s = string.Empty;
    node.TraversePreOrder(n => s += (string.IsNullOrWhiteSpace(s)) ? $"{n.Data}" : $" -> {n.Data}");
    s.Dump(nameof(TreeNodeExtensions.TraversePreOrder));

    s = string.Empty;
    node.TraversePostOrder(n => s += (string.IsNullOrWhiteSpace(s)) ? $"{n.Data}" : $" -> {n.Data}");
    s.Dump(nameof(TreeNodeExtensions.TraversePostOrder));
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

    public static void TraverseInOrder<T>(this TreeNode<T> node, Action<TreeNode<T>> action)
    {
        if (node == null) return;

        node.Left.TraverseInOrder(action);
        action?.Invoke(node);
        node.Right.TraverseInOrder(action);
    }

    public static void TraverseLevelOrder<T>(this TreeNode<T> node, Action<TreeNode<T>> action)
    {
        if (node == null) return;

        var queue = new Queue<TreeNode<T>>();
        queue.Enqueue(node);

        while (queue.Any())
        {
            TreeNode<T> n = queue.Dequeue();
            action?.Invoke(n);

            if (n.Left != null) queue.Enqueue(n.Left);
            if (n.Right != null) queue.Enqueue(n.Right);
        }
    }

    public static void TraversePostOrder<T>(this TreeNode<T> node, Action<TreeNode<T>> action)
    {
        if (node == null) return;

        node.Left.TraversePostOrder(action);
        node.Right.TraversePostOrder(action);
        action?.Invoke(node);
    }

    public static void TraversePreOrder<T>(this TreeNode<T> node, Action<TreeNode<T>> action)
    {
        if (node == null) return;

        action?.Invoke(node);
        node.Left.TraversePreOrder(action);
        node.Right.TraversePreOrder(action);
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

// 📖 https://www.programiz.com/dsa/tree-traversal
// 📖 https://stackoverflow.com/a/8567550/22944
// 📖 https://towardsdatascience.com/4-types-of-tree-traversal-algorithms-d56328450846
// 📖 https://www.geeksforgeeks.org/level-order-tree-traversal/
