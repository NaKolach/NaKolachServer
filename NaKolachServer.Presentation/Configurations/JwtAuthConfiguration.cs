namespace NaKolachServer.Presentation.Configurations;

public class JwtAuthConfiguration
{
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required string Key { get; set; }
}