<Query Kind="Program">
  <NuGetReference>MoreLinq</NuGetReference>
  <NuGetReference>System.Data.SQLite</NuGetReference>
  <NuGetReference>System.Data.SQLite.Core</NuGetReference>
  <NuGetReference>System.Data.SQLite.Linq</NuGetReference>
  <NuGetReference>System.Data.SQLite.EF6</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Data.Common</Namespace>
  <Namespace>System.Data.Entity</Namespace>
  <Namespace>System.Data.Entity.ModelConfiguration.Conventions</Namespace>
  <Namespace>System.Data.SQLite</Namespace>
  <AppConfig>
    <Path Relative="sqlite.app.config">D:\~dropBox\Dropbox\LinqPad\Queries\SQLite\sqlite.app.config</Path>
  </AppConfig>
</Query>

//http://www.bricelam.net/2012/10/entity-framework-on-sqlite.html
void Main()
{
    var root = Path.Combine(Environment.GetEnvironmentVariable("LOCALAPPDATA"),
        @"LINQPad\NuGet\ChinookDatabase.Sqlite\ChinookDatabase.Sqlite.1.4.0\content\net40\");
    AppDomain.CurrentDomain.SetData("DataDirectory", root);
    var connectionString = @"Data Source=|DataDirectory|Chinook.sqlite";
    var connection = new SQLiteConnection(connectionString);

    using (var context = new ChinookContext(connection))
    {
        context.Artists
            .Where(i => i.Name.StartsWith("A"))
            .OrderBy(i => i.Name)
            .ForEach(i =>
            {
                i.Name.Dump();
            });
    }
}

class ChinookContext : DbContext
{
    public ChinookContext(DbConnection existingConnection) : base(existingConnection, contextOwnsConnection: true)
    {
        //Database.SetInitializer(new Initial());
    }
    
    public DbSet<Artist> Artists { get; set; }

    public DbSet<Album> Albums { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        // Chinook Database does not pluralize table names
        modelBuilder.Conventions
            .Remove<PluralizingTableNameConvention>();
    }
}

public class Artist
{
    public Artist()
    {
        Albums = new List<Album>();
    }

    public long ArtistId { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Album> Albums { get; set; }
}

public class Album
{
    public long AlbumId { get; set; }
    public string Title { get; set; }

    public long ArtistId { get; set; }
    public virtual Artist Artist { get; set; }
}