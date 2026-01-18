using NaKolachServer.Domain.Utils;

namespace NaKolachServer.Domain.Points;

public class PointNotFoundException(string message) : BusinessException(message);