using Microsoft.AspNetCore.Http.HttpResults;

namespace Api;

public static class HouseEndpointsV1
{
    // Ref. https://github.com/dotnet/AspNetCore.Docs.Samples/blob/main/fundamentals/minimal-apis/samples/MinApiTestsSample/WebMinRouteGroup/TodoEndpointsV1.cs
    public static RouteGroupBuilder MapHousesApiV1(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAllHouses)
            .Produces<HouseDto[]>(StatusCodes.Status200OK);
    
        group.MapGet("/{houseId:int}", GetHouse).ProducesProblem(404).Produces<HouseDetailDto>(StatusCodes.Status200OK);

        return group;
    }

    public static Task<List<HouseDto>> GetAllHouses(IHouseRepository repo)
    {
        return repo.GetAll();
    }
    
    public static async Task<Results<Ok<HouseDetailDto>, ProblemHttpResult>> GetHouse(int houseId, IHouseRepository repo)
    {
        var house = await repo.Get(houseId);
        if (house == null) 
            return TypedResults.Problem($"House with ID {houseId} not found.", 
                statusCode: 404);
        return TypedResults.Ok(house);
    }
}