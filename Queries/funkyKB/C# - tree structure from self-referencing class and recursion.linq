<Query Kind="Program" />

void Main()
{
    var node = new TreeNode
    {
        Name = "one",
        Children = new List<TreeNode>
        {
            new TreeNode
            {
                Name = "two",
                Children = new List<TreeNode>
                {
                    new TreeNode { Name= "two-point-five" }
                }
            },
            new TreeNode { Name = "three" },
        }
    };

    node.ToDisplayText(s => s.Dump());
}

public static class TreeNodeExtensions
{
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