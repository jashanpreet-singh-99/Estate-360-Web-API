using EState_360.Core.Repositories;
using EState_360.Core.Services;
using EState_360.Infrastructure.Repositories;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddScoped<IListingRepository, InMemoryListingRepository>(); // Switched to CosmosDB
builder.Services.AddScoped<ListingService>();

// CosmosDB Config
builder.Services.AddSingleton<IMongoClient>(s =>
{
    var configuration = s.GetRequiredService<IConfiguration>();
    return new MongoClient(configuration["CosmosDb:ConnectionString"]); ;
});

builder.Services.AddScoped<IListingRepository>(s =>
{
    var mongoClient = s.GetRequiredService<IMongoClient>();
    return new CosmosMongoDbRepository(mongoClient, builder.Configuration["CosmosDb:DatabaseName"]!, builder.Configuration["CosmosDb:CollectionName"]!);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
