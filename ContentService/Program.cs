using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ContentService.Data;
using ContentService.MigService;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
    });
});
var server = builder.Configuration["DbServer"] ?? "ott";
var port = builder.Configuration["DbPort"] ?? "1433"; // Default SQL Server port
var user = builder.Configuration["DbUser"] ?? "SA"; // Warning do not use the SA account
var password = builder.Configuration["Password"] ?? "Ott@123";
var database = builder.Configuration["Database"] ?? "OTTPlatformContentsDB";

//concatenate them into a connection string
//server, port;Initial Catalog=database;userID=user;password=password
var connectionString = $"Server={server}, {port};Initial Catalog={database};User ID={user};Password={password};Encrypt=True;TrustServerCertificate=True;";

builder.Services.AddDbContext<ContentServiceContext>(options =>
    options.UseSqlServer((connectionString) ?? throw new InvalidOperationException("Connection string 'ProductsApiContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
DbMigrationService.MigrationInit(app);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
