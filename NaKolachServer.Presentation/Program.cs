using NaKolachServer.Presentation.Services;

using System.Data;

using Microsoft.EntityFrameworkCore;

using NaKolachServer.Domain.Users;
using NaKolachServer.Infrastructure;
using NaKolachServer.Application.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHttpClient<IOverpassService, OverpassService>();

builder.Services.AddOpenApi();

builder.Services.AddDbContext<DatabaseContext>(
    options => options
    .UseNpgsql(builder.Configuration["DATABASE_CONNECTION_STRING"]
        ?? throw new NoNullAllowedException("DATABASE_CONNECTION_STRING environment variable is null."))
    .UseSnakeCaseNamingConvention()
);

builder.Services.AddScoped<IUsersRepository, EFUsersRepository>();

builder.Services.AddScoped<GetUserById>();
builder.Services.AddScoped<InsertUser>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "NaKolachServer API v1");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
