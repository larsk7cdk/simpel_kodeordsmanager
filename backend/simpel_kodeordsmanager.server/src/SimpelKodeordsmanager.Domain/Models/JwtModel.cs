namespace SimpelKodeordsmanager.Domain.Models;

public class JwtModel
{
    public required string PrivateKeyPath { get; init; }
    
    public required string Key { get; init; }
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required int DurationInMinutes { get; init; }
}