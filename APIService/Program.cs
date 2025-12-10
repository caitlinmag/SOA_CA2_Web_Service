using APIService.Data;
using APIService.Models;
using APIService.Services;
using APIService.UserLogin;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Configuration;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration
    .GetSection("Jwt");
var key = (jwtSettings["Key"]);

//Authentication and JWT Bearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
                                            {
                                                ValidateIssuerSigningKey = true,
                                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                                                ValidateIssuer = false, 
                                                ValidateAudience = false,
                                                ValidateLifetime = true,
                                            };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddSingleton(res =>
res.GetRequiredService<IOptions<JwtSettings>>().Value);
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

builder.Services.AddSingleton<IMongoCollection<User>>(s =>
{
    var settings = s.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    var client = s.GetRequiredService<IMongoClient>();

    var database = client.GetDatabase(settings.DatabaseName);
    return database.GetCollection<User>(settings.UsersCollectionName);
});


builder.Services.AddSingleton<UserService>();
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

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
