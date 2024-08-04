using Api;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;

namespace Api.Tests.Unit;

[TestSubject(typeof(HouseEndpointsV1))]
public class HouseEndpointsV1Test
{
    private IHouseRepository _mockRepository;

    // Ref.
    // About how to create the EndpontsV1Test from a minial api project: https://github.com/dotnet/AspNetCore.Docs.Samples/blob/main/fundamentals/minimal-apis/samples/MinApiTestsSample/UnitTests/TodoMoqTests.cs
    // About how to use NSub: https://github.com/k90262/eShop/blob/main/tests/Basket.UnitTests/BasketServiceTests.cs
    public HouseEndpointsV1Test()
    {
        _mockRepository = Substitute.For<IHouseRepository>();
    }
    
    [Fact]
    public async Task GetAllHouses_RepoHasData_ReturnsHourses()
    {
        // Arrange
        GivenRepositoryHasListOfHouseDto();

        // Act
        var response = await HouseEndpointsV1.GetAllHouses(_mockRepository);
        
        // Assert
        await _mockRepository.Received().GetAll();
        Assert.Single(response);
    }

    private void GivenRepositoryHasListOfHouseDto()
    {
        List<HouseDto> items = [ new HouseDto(
            Id: 1,
            Address: "12 Valley of Kings, Geneva",
            Country: "Switzerland",
            Price: 900000)
        ];
        _mockRepository.GetAll().Returns(Task.FromResult(items));
    }

    [Fact]
    public async Task GetHouse_RepoHasData_ReturnsHourse()
    {
        // Arrange
        GivenRepositoryHasAHouseDetailDto();

        // Act
        var result = await HouseEndpointsV1.GetHouse(1, _mockRepository);
        
        // Assert
        Assert.IsType<Results<Ok<HouseDetailDto>, ProblemHttpResult>>(result);
        
        var okResult = (Ok<HouseDetailDto>) result.Result;
        Assert.NotNull(okResult.Value);
        Assert.Equal(1, okResult.Value.Id);
    }

    private void GivenRepositoryHasAHouseDetailDto()
    {
        HouseDetailDto item = new HouseDetailDto(
            Id: 1,
            Address: "12 Valley of Kings, Geneva",
            Country: "Switzerland",
            Price: 900000,
            Description: "A superb detached Victorian property on one of the town's finest roads, within easy reach of Barnes Village. The property has in excess of 6000 sq/ft of accommodation, a driveway and landscaped garden.",
            Photo: null
        );
        _mockRepository.Get(1)!.Returns(Task.FromResult(item));
    }

    [Fact]
    public async Task GetHouse_RepoHasNoData_Returns404()
    {
        // Arrange
        GivenRepositoryHasAHouseDetailDto();

        // Act
        var result = await HouseEndpointsV1.GetHouse(99, _mockRepository);
        
        // Assert
        Assert.IsType<Results<Ok<HouseDetailDto>, ProblemHttpResult>>(result);
        
        var notFoundResult = (ProblemHttpResult) result.Result;
        Assert.NotNull(notFoundResult);
        Assert.Equal(404, notFoundResult.StatusCode);
    }
}