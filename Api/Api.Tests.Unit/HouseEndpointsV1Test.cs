using Api;
using JetBrains.Annotations;
using NSubstitute;

namespace Api.Tests.Unit;

[TestSubject(typeof(HouseEndpointsV1))]
public class HouseEndpointsV1Test
{

    // Ref.
    // About how to create the EndpontsV1Test from a minial api project: https://github.com/dotnet/AspNetCore.Docs.Samples/blob/main/fundamentals/minimal-apis/samples/MinApiTestsSample/UnitTests/TodoMoqTests.cs
    // About how to use NSub: https://github.com/k90262/eShop/blob/main/tests/Basket.UnitTests/BasketServiceTests.cs
    
    [Fact]
    public async Task GetAllHouses_RepoHasData_ReturnsHourses()
    {
        // Arrange
        var mockRepository = Substitute.For<IHouseRepository>();
        List<HouseDto> items = [ new HouseDto(
            Id: 1,
            Address: "12 Valley of Kings, Geneva",
            Country: "Switzerland",
            Price: 900000)
        ];
        mockRepository.GetAll().Returns(Task.FromResult(items));
        
        // Act
        var response = await HouseEndpointsV1.GetAllHouses(mockRepository);
        
        // Assert
        await mockRepository.Received().GetAll();
        Assert.Single(response);
    }
    
    [Fact]
    public void GetHouse_RepoHasData_ReturnsHourse()
    {
        //TODO
    }
    
    [Fact]
    public void GetHouse_RepoHasNoData_Returns404()
    {
        //TODO
    }
}