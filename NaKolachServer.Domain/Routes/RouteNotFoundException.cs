using NaKolachServer.Domain.Utils;

namespace NaKolachServer.Domain.Routes;

public class RouteNotFoundException(string message) : BusinessException(message);