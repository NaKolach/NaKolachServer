using System.Security.Claims;

namespace NaKolachServer.Presentation.Utils;

public static class ClaimsExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var idString = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (Guid.TryParse(idString, out var id)) return id;

        throw new UnauthorizedAccessException("User ID claim is missing or invalid.");
    }
}