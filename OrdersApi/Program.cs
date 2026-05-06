using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = "authapi",
            ValidAudience = "microservices",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("this-is-my-super-secret-key-for-jwt-demo"))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/orders", () =>
{
    return new[]
    {
        new { Id = 1, Name = "Laptop" },
        new { Id = 2, Name = "Mouse" }
    };
})
.RequireAuthorization();
//.WithName("GetProducts")
//.WithTags("Products");

app.Run();
