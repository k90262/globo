using Api;
using JetBrains.Annotations;

namespace Api.Tests.Unit;

[TestSubject(typeof(HouseEndpointsV1))]
public class HouseEndpointsV1Test
{

    // Ref. https://github.com/dotnet/AspNetCore.Docs.Samples/blob/main/fundamentals/minimal-apis/samples/MinApiTestsSample/UnitTests/TodoMoqTests.cs
    [Fact]
    public void GetAllHouses_RepoHasData_ReturnsHourses()
    {
        //TODO
    }
    
    [Fact]
    public void GetAllHouses_RepoHasNoData_ReturnsEmptyList()
    {
        //TODO
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