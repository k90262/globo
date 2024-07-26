using Microsoft.EntityFrameworkCore;

public class HouseDbContext : DbContext 
{
    public DbSet<HouseEntity> Hourses => Set<HouseEntity>();

    public HouseDbContext(DbContextOptions<HouseDbContext> o): 
        base(o) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        SeedData.Seed(builder);
    }
}