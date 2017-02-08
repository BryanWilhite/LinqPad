<Query Kind="Program">
  <NuGetReference>WindowsAzure.Storage</NuGetReference>
  <Namespace>Microsoft.WindowsAzure</Namespace>
  <Namespace>Microsoft.WindowsAzure.Storage</Namespace>
  <Namespace>Microsoft.WindowsAzure.Storage.Blob</Namespace>
</Query>

/*
    From “Developing Cloud Applications with Windows Azure Storage: Blobs”
    by Paul Mehner
    [https://www.microsoftpressstore.com/articles/article.aspx?p=2224058&seqNum=6#.VY4rpFM4Uqs.twitter]
*/
void Main()
{
    var secret = Util.CurrentQuery.GetLinqPadMetaSecret("azure", "storage");
    var account = CloudStorageAccount.Parse(secret);
    DirectoryHierarchies(account);
}

public static void DirectoryHierarchies(CloudStorageAccount account)
{
    var client = account.CreateCloudBlobClient();
    Console.WriteLine("Default delimiter={0}", client.DefaultDelimiter /* settable */);
    Console.WriteLine();

    // Create the virtual directory
    const String virtualDirName = "demo";
    CloudBlobContainer virtualDir = client
        .GetContainerReference(virtualDirName)
        .EnsureExists(true);

    // Create some file entries under the virtual directory
    var virtualFiles = new String[] {
        "FileA", "FileB", // Avoid  $&+,/:=?@ in blob names
        "Dir1/FileC", "Dir1/FileD", "Dir1/Dir2/FileE",
        "Dir3/FileF", "Dir3/FileG",
        "Dir4/FileH"
    };

    foreach (String file in virtualFiles)
    {
        virtualDir.GetBlockBlobReference("Root/" + file).UploadText(String.Empty);
    }

    // Show the blobs in the virtual directory container
    ShowContainerBlobs(virtualDir);   // Same as UseFlatBlobListing = false
    Console.WriteLine();
    ShowContainerBlobs(virtualDir, true);

    // CloudBlobDirectory (derived from IListBlobItem) is for traversing
    // and accessing blobs with names structured in a directory hierarchy.
    CloudBlobDirectory root = virtualDir.GetDirectoryReference("Root");
    WalkBlobDirHierarchy(root, 0);

    // Show just the blobs under Dir1
    Console.WriteLine();
    String subdir = virtualDir.Name + "/Root/Dir1/";
    foreach (var file in client.ListBlobs(subdir)) Console.WriteLine(file.Uri);
}

static void ShowContainerBlobs(CloudBlobContainer container,
     Boolean useFlatBlobListing = false, BlobListingDetails details = BlobListingDetails.None,
          BlobRequestOptions options = null, OperationContext operationContext = null)
{
    Console.WriteLine("Container: " + container.Name);
    var brs = container.ListBlobsSegmented(null, useFlatBlobListing, details, 1000,
        null, options, operationContext);
    while (brs.ContinuationToken != null)
    {
        brs = container.ListBlobsSegmented(null,
            useFlatBlobListing, details, 1000,
            brs.ContinuationToken, options, operationContext);
        foreach (var blob in brs.Results) Console.WriteLine("   " + blob.Uri);
    }
}

static void WalkBlobDirHierarchy(CloudBlobDirectory dir, Int32 indent)
{
    // Get all the entries in the root directory
    IListBlobItem[] entries = dir.ListBlobs().ToArray();
    String spaces = new String(' ', indent * 3);

    Console.WriteLine(spaces + dir.Prefix + " entries:");
    foreach (var entry in entries.OfType<ICloudBlob>())
        Console.WriteLine(spaces + "   " + entry.Name);

    foreach (var entry in entries.OfType<CloudBlobDirectory>())
    {
        String[] segments = entry.Uri.Segments;
        CloudBlobDirectory subdir = dir.GetDirectoryReference(segments[segments.Length - 1]);
        WalkBlobDirHierarchy(subdir, indent + 1); // Recursive call
    }
}

static void ShowContainer(CloudBlobContainer container, Boolean showBlobs)
{
    Console.WriteLine("Blob container={0}", container);

    BlobContainerPermissions permissions = container.GetPermissions();
    String[] meanings = new String[] {
                            "no public access",
                            "anonymous clients can read container & blob data",
                            "anonymous readers can read blob data only"
                    };
    Console.WriteLine("Container's public access={0} ({1})",
       permissions.PublicAccess, meanings[(Int32)permissions.PublicAccess]);

    // Show collection of access policies; each consists of name & SharedAccesssPolicy
    // A SharedAccesssBlobPolicy contains:
    //    SharedAccessPermissions enum (None, Read, Write, Delete, List) &
    //    SharedAccessStartTime/SharedAccessExpireTime
    Console.WriteLine("   Shared access policies:");
    foreach (var policy in permissions.SharedAccessPolicies)
    {
        Console.WriteLine("   {0}={1}", policy.Key, policy.Value);
    }

    container.FetchAttributes();
    Console.WriteLine("   Attributes: Name={0}, Uri={1}", container.Name, container.Uri);
    Console.WriteLine("   Properties: LastModified={0}, ETag={1},",
         container.Properties.LastModified, container.Properties.ETag);
    ShowMetadata(container.Metadata);

    if (showBlobs)
        foreach (ICloudBlob blob in container.ListBlobs())
            ShowBlob(blob);
}

static void ShowBlob(ICloudBlob blob)
{
    // A blob has attributes: Uri, Snapshot DateTime?, Properties & Metadata
    // The CloudBlob Uri/SnapshotTime/Properties/Metadata properties return these
    // You can set the properties & metadata; not the Uri or snapshot time
    Console.WriteLine("Blob Uri={0}, Snapshot time={1}", blob.Uri, blob.SnapshotTime);
    BlobProperties bp = blob.Properties;
    Console.WriteLine("BlobType={0}, CacheControl={1}, Encoding={2}, Language={3}, MD5={4}, ContentType={5}, LastModified={6}, Length={7}, ETag={8}",
       bp.BlobType, bp.CacheControl, bp.ContentEncoding, bp.ContentLanguage,
            bp.ContentMD5, bp.ContentType, bp.LastModified, bp.Length, bp.ETag);
    ShowMetadata(blob.Metadata);
}

static void ShowMetadata(IDictionary<String, String> metadata)
{
    foreach (var kvp in metadata)
        Console.WriteLine("{0}={1}", kvp.Key, kvp.Value);
}

public static class CloudBlobContainerExtensions
{
    public static CloudBlobContainer EnsureExists(this CloudBlobContainer container, bool publicContainer)
    {
        //container.CreateIfNotExist();
        var permissions = new BlobContainerPermissions();

        if (publicContainer)
        {
            permissions.PublicAccess = BlobContainerPublicAccessType.Container;
        }

        container.SetPermissions(permissions);

        return container;
    }

    public static bool HasMoreResults<T>(this ResultSegment<T> resultSegment)
    {
        return (resultSegment == null) ? true : resultSegment.HasMoreResults();
    }
}