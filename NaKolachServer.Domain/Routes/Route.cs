namespace NaKolachServer.Domain.Routes;

public record Route
(
    Guid Id,
    Guid AuthorId,
    double Distance,
    long Time,
    string Path,
    string[] Categories,
    string Points,
    DateTimeOffset CreatedAt
);