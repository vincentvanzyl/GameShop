using System.Security.Claims;
using System.Text;
using GamesGlobal.Api.Attributes;
using GamesGlobal.Core.Ioc;
using GamesGlobal.Utilities.Config;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Okta.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCoreServices();
GamesGlobalSettings.SetInstance(builder.Configuration);

var securityKey = GamesGlobalSettings.Instance.Security.StrongKey; // Should be in config
var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            //What to validate
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            //Setup validation data
            ValidIssuer = "GamesGlobal",
            ValidAudience = "readers",
            IssuerSigningKey = symmetricSecurityKey
        };
    });
builder.Services.AddAuthorization(config =>
{
    config.AddPolicy("Role", 
        options => options.RequireClaim(ClaimTypes.Role, "User"));
});

builder.Services.AddAuthorization();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<HandleKnownErrorFilterAttribute>();
});

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