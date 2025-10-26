namespace SimpelKodeordsmanager.Domain.Models;

public sealed class Jwt
{
    public required string PrivateKeyPath { get; init; }
    
    public required string Key { get; init; }
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required int DurationInMinutes { get; init; }
}