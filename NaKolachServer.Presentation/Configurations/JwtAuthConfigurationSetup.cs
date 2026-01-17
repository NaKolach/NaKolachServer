using System.Data;

using Microsoft.Extensions.Options;

namespace NaKolachServer.Presentation.Configurations;

public class JwtAuthConfigurationSetup(IConfiguration configuration)
    : IConfigureOptions<JwtAuthConfiguration>
{
    public void Configure(JwtAuthConfiguration options)
    {
        options.Issuer = configuration["AUTH_ISSUER"] ?? throw new NoNullAllowedException("AUTH_ISSUER environment variable is null.");
        options.Audience = configuration["AUTH_AUDIENCE"] ?? throw new NoNullAllowedException("AUTH_AUDIENCE environment variable is null.");
        options.Key = configuration["AUTH_KEY"] ?? throw new NoNullAllowedException("AUTH_KEY environment variable is null.");
    }
}