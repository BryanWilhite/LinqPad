<Query Kind="Program">
  <Namespace>System.Security.AccessControl</Namespace>
</Query>

public class FileStream
{
	public FileStream (string path, FileMode mode)
		: this (path, mode, FileAccess.ReadWrite) { }
		
	public FileStream (string path, FileMode mode, FileAccess access)
		: this (path, mode, access, FileShare.Read) { }
	
	public FileStream (string path, FileMode mode, FileAccess access, FileShare share)
		: this (path, mode, access, share, 0x1000) { }
	
	public FileStream (string path, FileMode mode, FileAccess access, FileShare share, int bufferSize)
		: this (path, mode, access, share, bufferSize, FileOptions.None) { }
	
	public FileStream (string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options)
	{
		// Actual code!!!
		// ...
	}
}	

void Main() { }