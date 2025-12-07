using APIService.Models;
using APIService.Data;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using APIService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<IMongoClient>(s =>
{
    var settings = s.GetRequiredService<IOptions<MongoDbSettings>> ().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddSingleton<IMongoCollection<DrinkItem>>(s =>
{
    var settings = s.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    var client = s.GetRequiredService<IMongoClient>();

    var database = client.GetDatabase(settings.DatabaseName);
    return database.GetCollection<DrinkItem>(settings.DrinksCollectionName);
});

builder.Services.AddSingleton<IMongoCollection<DrinksSales>>(s =>
{
    var settings = s.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    var client = s.GetRequiredService<IMongoClient>();

    var database = client.GetDatabase(settings.DatabaseName);
    return database.GetCollection<DrinksSales>(settings.SalesCollectionName);
});

builder.Services.AddSingleton<IMongoCollection<Supplier>>(s =>
{
    var settings = s.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    var client = s.GetRequiredService<IMongoClient>();

    var database = client.GetDatabase(settings.DatabaseName);
    return database.GetCollection<Supplier>(settings.SuppliersCollectionName);
});

builder.Services.AddSingleton<DrinksService>();
builder.Services.AddSingleton<DrinkSaleService>();
builder.Services.AddSingleton<SupplierService>();


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
