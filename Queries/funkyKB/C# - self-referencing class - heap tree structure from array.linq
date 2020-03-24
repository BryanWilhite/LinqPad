<Query Kind="Program" />

void Main()
{
    var array = new[] { 4, 10, 3, 5, 1 };

    var node = new TreeNode();

    node.Set(array, 0);

    node?.ToDisplayText(s => s.Dump());
}

public static class TreeNodeExtensions
{
    public static void Set<TArray>(this TreeNode node, TArray[] array, int startIndex)
    {
        var indexLeftChild = (2 * startIndex + 1);
        var indexRightChild = (2 * startIndex + 2);
        $"{startIndex}: {nameof(indexLeftChild)}: {indexLeftChild}; {nameof(indexRightChild)}: {indexRightChild}".Dump();

        node.Name = $"node {array[startIndex]}";

        if (indexRightChild < array.Length)
        {
            node.Children.Add(new TreeNode { Name = $"node {array[indexLeftChild]}" });
            node.Children[0].Set(array, ++startIndex);

            node.Children.Add(new TreeNode { Name = $"node {array[indexRightChild]}" });
            node.Children[1].Set(array, ++startIndex);
        }
    }

    public static void ToDisplayText(this TreeNode node, Action<string> displayAction)
    {
        node.ToDisplayText("  ", false, displayAction);
    }

    public static void ToDisplayText(this TreeNode node, string indent, bool isLast, Action<string> displayAction)
    {
        displayAction?.Invoke($"{indent}+- {node.Name}");
        indent += isLast ? "   " : "|  ";

        for (int i = 0; i < node.Children.Count; i++)
        {
            node.Children[i].ToDisplayText(indent, i == node.Children.Count - 1, displayAction);
        }
    }
}

public class TreeNode
{
    public string Name;

    public List<TreeNode> Children = Enumerable.Empty<TreeNode>().ToList();
}

// 📖 https://stackoverflow.com/a/8567550/22944
// 📖 https://www.geeksforgeeks.org/building-heap-from-array/