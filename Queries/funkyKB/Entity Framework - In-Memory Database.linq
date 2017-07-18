<Query Kind="Program">
  <NuGetReference>Microsoft.EntityFrameworkCore.InMemory</NuGetReference>
  <Namespace>Microsoft.EntityFrameworkCore</Namespace>
</Query>

/*
    https://docs.microsoft.com/en-us/ef/core/providers/in-memory/
    https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory/
*/
void Main()
{
    var options = new DbContextOptionsBuilder<MyInMemoryContext>()
        .UseInMemoryDatabase(databaseName: "MyInMemoryDb")
        .Options;

    using (var dbContext = new MyInMemoryContext(options))
    {
        dbContext.Database.Dump("dbContext.Database");
        dbContext.LoadData();

        var data = dbContext.Products
            .Where(i => i.Category == "Canned")
            .ToArray();

        data.Dump("products");
    }
}

public class MyInMemoryContext : DbContext
{
    public MyInMemoryContext()
    { }

    public MyInMemoryContext(DbContextOptions<MyInMemoryContext> options)
        : base(options)
    { }

    public DbSet<Product> Products { get; set; }
}

public static class MyInMemoryContextExtensions
{
    public static void LoadData(this MyInMemoryContext dbContext)
    {
        if (dbContext == null) return;

        var data = new[]
        {
            new Product { Id = 1, Category = "Bakery", Name = "Spinach/Rice Tortillas", Price = 6.99M },
            new Product { Id = 2, Category = "Bakery", Name = "Organic Corn Tortillas", Price = 1.99M },
            new Product { Id = 3, Category = "Bakery", Name = "Flower Flour Thins", Price = 5.99M },
            new Product { Id = 4, Category = "Canned", Name = "Ranchero Beans", Price = 1.69M },
            new Product { Id = 5, Category = "Canned", Name = "Wild Caught Sardines in Water", Price = 3.99M },
            new Product { Id = 6, Category = "Canned", Name = "Wild Caught Salmon in Water", Price = 6.99M },
            new Product { Id = 7, Category = "Grocery", Name = "Organic Swiss Chard", Price = 2.89M },
            new Product { Id = 8, Category = "Grocery", Name = "Organic Kale", Price = 2.99M },
            new Product { Id = 9, Category = "Grocery", Name = "Organic Red Potatoes", Price = 2.49M },
        };

        dbContext.Products.AddRange(data);
        dbContext.SaveChanges();
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
}
