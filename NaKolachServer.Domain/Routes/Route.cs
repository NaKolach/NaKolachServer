using System.Drawing;

namespace NaKolachServer.Domain.Routes;

public record Route
(
    Guid Id,
    Guid AuthorId,
    Path Path,
    Point[] Points,
    DateTimeOffset CreatedAt
);