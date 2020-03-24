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
    public static void Set<TArray>(this TreeNode node, TArray[] array, int treeNodeLevel)
    {
        var indexOfParent = (treeNodeLevel - 1) / 2;
        var indexLeftChild = (2 * treeNodeLevel + 1);
        var indexRightChild = (2 * treeNodeLevel + 2);

        var msg = $"{nameof(treeNodeLevel)}: {treeNodeLevel}; {nameof(indexOfParent)}: {indexOfParent}; {nameof(indexLeftChild)}: {indexLeftChild}; {nameof(indexRightChild)}: {indexRightChild}";

        node.Name = $"node {array[indexOfParent]} [{msg}]";

        if (treeNodeLevel < Convert.ToInt32(Math.Ceiling(array.Length/3M)))
        {
            node.Children.Add(new TreeNode { Name = $"node {array[indexLeftChild]} [{msg}]" });
            //node.Children[0].Set(array, ++treeNodeLevel);

            node.Children.Add(new TreeNode { Name = $"node {array[indexRightChild]} [{msg}]" });
            //node.Children[1].Set(array, ++treeNodeLevel);
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
// 📖 https://evelynn.gitbooks.io/amazon-interview/serialize-and-deserialize-binary-tree.html