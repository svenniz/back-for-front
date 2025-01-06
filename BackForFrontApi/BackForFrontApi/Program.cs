using AutoMapper;
using BackForFrontApi.Data_Access;
using BackForFrontApi.Dtos;
using BackForFrontApi.Repositories;
using BackForFrontApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// appsettings.json variable for whether or not using inmemory
var isInMemoryDatabase = builder.Configuration.GetValue<bool>("UseInMemoryDatabase");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(typeof(MappingProfile));

// Injecting services
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericEfCoreRepository<>));
builder.Services.AddScoped<IHouseRepository, HouseRepository>();

// Configure CORS to allow any origin
builder.Services.AddCors();
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", builder =>
//    {
//        builder.AllowAnyOrigin()
//               .AllowAnyMethod()
//               .AllowAnyHeader();
//    });
//});


// Add DbContext:
builder.Services.AddDbContext<HouseDbContext>(options =>
{
    if (isInMemoryDatabase)
    {
        options.UseInMemoryDatabase("HouseDatabase");
        Console.WriteLine("Database is InMemory");
    }
    else
    {
        // Configure DbContext with SQLite
        // Folders for Sqlite Db
        var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SqliteDbs");
        Directory.CreateDirectory(folder);
        var path = Path.Combine(folder, "houses.db");
        options.UseSqlite($"Data Source={path}");
    }
});

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

app.MapGet("/houses", (IHouseRepository houseRepository, IMapper mapper) =>
    houseRepository.GetAllHousesWithMap());

app.UseAuthorization();

// app.UseCors("AllowAll");

app.MapControllers();

app.Run();