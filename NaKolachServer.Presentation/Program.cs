using NaKolachServer.Presentation.Services;

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

// builder.Services.AddHttpClient<IOverpassService, OverpassService>();
// builder.Services.AddHttpClient<IRouteProvider, GraphhopperRouteProvider>();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IUsersRepository, EFUsersRepository>();
builder.Services.AddScoped<IPointsRepository, EFPointsRepository>();
builder.Services.AddScoped<IRouteProvider, GraphhopperRouteProvider>();

builder.Services.AddScoped<GetUserById>();
builder.Services.AddScoped<InsertUser>();
builder.Services.AddScoped<PasswordService>();

builder.Services.AddScoped<FileService>();

builder.Services.AddScoped<IRegisterService, RegisterService>();

builder.Services.AddScoped<GetPoints>();

builder.Services.AddScoped<CalculateRoute>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "NaKolachServer API v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
