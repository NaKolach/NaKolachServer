using System.Data;

using Microsoft.EntityFrameworkCore;

using NaKolachServer.Domain.Users;
using NaKolachServer.Application.Users;
using NaKolachServer.Infrastructure.Database.Business;
using NaKolachServer.Infrastructure.Database.OSM;
using NaKolachServer.Application.Points;
using NaKolachServer.Domain.Points;
using NaKolachServer.Application.Routes;
using NaKolachServer.Domain.Routes;
using NaKolachServer.Infrastructure.RouteProviders;
using NaKolachServer.Presentation.Utils;
using NaKolachServer.Application.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NaKolachServer.Domain.Auth;
using NaKolachServer.Presentation.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddDbContext<DatabaseContext>(
    options => options
    .UseNpgsql(builder.Configuration["DATABASE_CONNECTION_STRING"]
        ?? throw new NoNullAllowedException("DATABASE_CONNECTION_STRING environment variable is null."))
    .UseSnakeCaseNamingConvention()
);

builder.Services.AddDbContext<OSMDatabaseContext>(
    options => options
    .UseNpgsql(builder.Configuration["OSM_DATABASE_CONNECTION_STRING"]
        ?? throw new NoNullAllowedException("OSM_DATABASE_CONNECTION_STRING environment variable is null."),
        options => options.UseNetTopologySuite())
    .UseSnakeCaseNamingConvention()
);

builder.Services.ConfigureOptions<JwtAuthConfigurationSetup>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["AUTH_ISSUER"]
        ?? throw new InvalidOperationException("AUTH_ISSUER environment variable is null."),
        ValidAudience = builder.Configuration["AUTH_AUDIENCE"]
        ?? throw new InvalidOperationException("AUTH_AUDIENCE environment variable is null."),
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["AUTH_KEY"]
            ?? throw new InvalidOperationException("AUTH_KEY environment variable is null."))
        )
    };
});

builder.Services.AddHttpClient();

builder.Services.AddSingleton<IJwtTokenProvider, JwtTokenProvider>();
builder.Services.AddScoped<IUserPasswordHasher, UserPasswordHasher>();

builder.Services.AddScoped<IAuthRepository, EFAuthRepository>();
builder.Services.AddScoped<IUsersRepository, EFUsersRepository>();
builder.Services.AddScoped<IPointsRepository, EFPointsRepository>();
builder.Services.AddScoped<IRouteProvider, GraphhopperRouteProvider>();

builder.Services.AddScoped<GetUserById>();

builder.Services.AddScoped<RegisterUser>();
builder.Services.AddScoped<VerifyUserCredentials>();
builder.Services.AddScoped<RefreshUserCredential>();

builder.Services.AddScoped<GetPoints>();

builder.Services.AddScoped<CalculateRoute>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "NaKolachServer API v1"));
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
