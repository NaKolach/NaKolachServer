namespace NaKolachServer.Domain.Auth;

public record RefreshToken(
    Guid Id,
    Guid UserId,
    string Token,
    bool IsRevoked,
    DateTimeOffset ExpiresAt
);