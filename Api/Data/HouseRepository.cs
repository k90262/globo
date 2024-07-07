using Microsoft.EntityFrameworkCore;

public interface IHouseRepository
{
    Task<List<HouseDto>> GetAll();
    Task<HouseDetailDto?> Get(int id);
}

public class HouseRepository : IHouseRepository
{
    private readonly HouseDbContext context;

    public HouseRepository(HouseDbContext context)
    {
        this.context = context;
    }

    public async Task<List<HouseDto>> GetAll() 
    {
        return await context.Hourses.Select(h => new HouseDto(h.Id, h.Address, h.Country, h.Price)).ToListAsync();
    }

    public async Task<HouseDetailDto?> Get(int id)
    {
        var e = await context.Hourses.SingleOrDefaultAsync(
            h => h.Id == id);
        if (e == null) 
            return null;
        return new HouseDetailDto(e.Id, e.Address, e.Country, e.Price, e.Description, e.Photo);
    }
}