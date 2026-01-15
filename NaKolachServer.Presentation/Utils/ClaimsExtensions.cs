using System.Security.Claims;

using NaKolachServer.Domain.Users;

using Newtonsoft.Json;

namespace NaKolachServer.Presentation.Utils;

public static class ClaimsExtensions
{
    public static UserContext GetContext(this ClaimsPrincipal user)
    {
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? user.FindFirstValue("sub")
            ?? throw new Exception("Token is missing sub.");

        var login = user.FindFirst(ClaimTypes.Name)?.Value
            ?? user.FindFirstValue("name")
            ?? throw new Exception("Token is missing name.");

        Console.WriteLine("XD ", userId, login);

        return new UserContext(Guid.Parse(userId), login);
    }
}