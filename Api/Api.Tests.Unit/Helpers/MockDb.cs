using Microsoft.EntityFrameworkCore;

namespace Api.Tests.Unit.Helpers;

public class MockDb : IDbContextFactory<HouseDbContext>
{
    public HouseDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<HouseDbContext>()
            .UseInMemoryDatabase(databaseName: $"InMemoryTestDb-{DateTime.Now.ToFileTimeUtc()}")
            .Options;

        return new HouseDbContext(options);
    }
}