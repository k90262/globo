using Api.Tests.Unit.Helpers;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Api.Tests.Unit.Data;

[TestSubject(typeof(HouseRepository))]
public class HouseRepositoryTest
{
    private HouseDbContext _context = null!;
    private List<HouseEntity> _storedList = null!;
    private List<HouseDto> _expectedList = null!;
    private List<HouseDetailDto> _expectedDetailList = null!;

    [Fact]
    public async void GetAll_DataExist_ReturnsDataDto()
    {
        // Arrange
        await GivenInMemoryDatabaseWithData();

        // Act
        var repository = new HouseRepository(_context);
        var actualList = await repository.GetAll();

        // Assert
        Assert.Equal(_expectedList, actualList);
    }

    private async Task GivenInMemoryDatabaseWithData()
    {
        _context = new MockDb().CreateDbContext();
        await GivenHouseEntities();
        
        _expectedList = _storedList.Select(h => new HouseDto(h.Id, h.Address, h.Country, h.Price)).ToList();
        _expectedDetailList = _storedList.Select(e => new HouseDetailDto(e.Id, e.Address, e.Country, e.Price, e.Description, e.Photo)).ToList();

    }

    private async Task GivenHouseEntities()
    {
        _storedList = new List<HouseEntity> {
            new HouseEntity {
                Id = 1,
                Address = "12 Valley of Kings, Geneva",
                Country = "Switzerland",
                Description = "A superb detached Victorian property on one of the town's finest roads, within easy reach of Barnes Village. The property has in excess of 6000 sq/ft of accommodation, a driveway and landscaped garden.",
                Price = 900000
            },
            new HouseEntity
            {
                Id = 2,
                Address = "89 Road of Forks, Bern",
                Country = "Switzerland",
                Description = "This impressive family home, which dates back to approximately 1840, offers original period features throughout and is set back from the road with off street parking for up to six cars and an original Coach House, which has been incorporated into the main house to provide further accommodation. ",
                Price = 500000
            },
            new HouseEntity
            {
                Id = 3,
                Address = "Grote Hof 12, Amsterdam",
                Country = "The Netherlands",
                Description = "This house has been designed and built to an impeccable standard offering luxurious and elegant living. The accommodation is arranged over four floors comprising a large entrance hall, living room with tall sash windows, dining room, study and WC on the ground floor.",
                Price = 200500
            },
            new HouseEntity
            {
                Id = 4,
                Address = "Meel Kade 321, The Hague",
                Country = "The Netherlands",
                Description = "Discreetly situated a unique two storey period home enviably located on the corner of Krom Road and Recht Road offering seclusion and privacy. The house features a magnificent double height reception room with doors leading directly out onto a charming courtyard garden.",
                Price = 259500
            },
            new HouseEntity
            {
                Id = 5,
                Address = "Oude Gracht 32, Utrecht",
                Country = "The Netherlands",
                Description = "This luxurious three bedroom flat is contemporary in style and benefits from the use of a gymnasium and a reserved underground parking space.",
                Price = 400500
            }};

        foreach (var houseEntity in _storedList)
        {
            _context.Add(houseEntity);
        }
        await _context.SaveChangesAsync();
    }

    [Fact]
    public async void GetAll_NoData_ReturnsEmptyList()
    {
        // Arrange
        GivenEmptyInMemoryDatabase();
        
        // Act
        var repository = new HouseRepository(_context);
        var actualList = await repository.GetAll();

        // Assert
        Assert.Empty(actualList);
    }

    private void GivenEmptyInMemoryDatabase()
    {
        _context = new MockDb().CreateDbContext();
    }

    [Fact]
    public async void Get_DataExist_ReturnsDataDto()
    {
        // Arrange
        await GivenInMemoryDatabaseWithData();

        // Act
        var repository = new HouseRepository(_context);
        var actual = await repository.Get(2);

        // Assert
        var expected = _expectedDetailList.Find(dto => dto.Id == 2);
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public async void Get_NoData_ReturnsNull()
    {
        // Arrange
        GivenEmptyInMemoryDatabase();

        // Act
        var repository = new HouseRepository(_context);
        var actual = await repository.Get(2);

        // Assert
        Assert.Null(actual);
    }
}