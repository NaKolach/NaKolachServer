using System.Security.Claims;

using Newtonsoft.Json;

namespace NaKolachServer.Presentation.Utils;

public static class ClaimsExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var idString = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? user.FindFirstValue("sub");

        Console.WriteLine("XD ", string.Join(", ", user.Claims.Select(c => new { c.Type, c.Value })));

        if (Guid.TryParse(idString, out var id)) return id;

        throw new UnauthorizedAccessException("User ID claim is missing or invalid.");
    }
}