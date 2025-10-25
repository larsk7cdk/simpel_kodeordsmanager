namespace SimpelKodeordsmanager.Application.Exceptions;

public abstract class NotFoundException(string name, object key) : Exception($"{name} with key {key} not found");