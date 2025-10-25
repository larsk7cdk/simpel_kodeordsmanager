namespace SimpelKodeordsmanager.Application.Exceptions;

public class UserNotFoundException(string email) : Exception($"User with Email {email} not found");