using Api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContext<HouseDbContext>(o =>
{
    o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    
    // Ref. https://github.com/dotnet/AspNetCore.Docs.Samples/blob/main/fundamentals/minimal-apis/samples/MinApiTestsSample/WebMinRouteGroup/Program.cs
    var folder = Environment.SpecialFolder.LocalApplicationData; // About this path on MacOSX, see https://github.com/dotnet/runtime/issues/40353
    var path = Environment.GetFolderPath(folder);                // "/Users/apple/Library/Application Support/"
    o.UseSqlite($"Data Source={Path.Join(path, "globo_houses.db")}");
});
builder.Services.AddScoped<IHouseRepository, HouseRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(p => p.WithOrigins("http://localhost:3000")
    .AllowAnyHeader().AllowAnyMethod());
app.UseHttpsRedirection();

app.MapGroup("/houses/v1")
    .MapHousesApiV1()
    .WithTags("Houses Endpoints");

app.Run();
