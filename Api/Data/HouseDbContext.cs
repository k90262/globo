using Microsoft.EntityFrameworkCore;

public class HouseDbContext : DbContext 
{
    public DbSet<HouseEntity> Hourses => Set<HouseEntity>();

    public HouseDbContext(DbContextOptions<HouseDbContext> o): 
        base(o) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData; // About this path on MacOSX, see https://github.com/dotnet/runtime/issues/40353
        var path = Environment.GetFolderPath(folder); // "/Users/apple/Library/Application Support/"
        optionsBuilder
            .UseSqlite($"Data Source={Path.Join(path, "globo_houses.db")}");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        SeedData.Seed(builder);
    }
}