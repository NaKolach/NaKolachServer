using NaKolachServer.Domain.Utils;

namespace NaKolachServer.Domain.Users;

public class UserNotFoundException(string message) : BusinessException(message);