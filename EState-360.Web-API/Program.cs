using EState_360.Core.Repositories;
using EState_360.Core.Services;
using EState_360.Infrastructure.Repositories;
using EState_360.Web_API.Mappings;
using EState_360.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IListingRepository, CosmosMongoDbRepository>();
builder.Services.AddScoped<ListingService>();

// CosmosDB Config
builder.Services.AddSingleton(s =>
{
    var configuration = s.GetRequiredService<IConfiguration>();
    return new MongoDbContext(configuration);
});

// Add Auto Mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

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
