void Main()
{
    var path = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "Microsoft", "Internet Explorer"
    );
    if (!Directory.Exists(path)) throw new DirectoryNotFoundException("The expected directory is not here. Cannot run.");

    var parentName = Environment.UserName;
    parentName.Dump($"{nameof(parentName)}");

    var info = FrameworkFileUtility.FindParentDirectory(path, parentName, levels: 5);
    info.Dump();
}

static class FrameworkFileUtility
{
    public static DirectoryInfo FindParentDirectory(string path, string parentName, int levels)
    {
        if (string.IsNullOrEmpty(path)) throw new DirectoryNotFoundException("The expected directory is not here.");

        var info = new DirectoryInfo(path);

        var isParent = (info.Name == parentName);
        var hasNullParent = (info.Parent == null);
        var hasTargetParent = (info.Parent.Name == parentName);

        if (!info.Exists) return null;
        if (isParent) return info;
        if (hasNullParent) return null;
        if (hasTargetParent) return info.Parent;

        levels = Math.Abs(levels);
        --levels;

        var hasNoMoreLevels = (levels == 0);

        if (hasNoMoreLevels) return null;

        return FindParentDirectory(info.Parent.FullName, parentName, levels);
    }
}
