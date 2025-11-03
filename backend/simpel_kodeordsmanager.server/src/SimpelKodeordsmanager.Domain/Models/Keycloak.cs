namespace SimpelKodeordsmanager.Domain.Models;

public class Keycloak
{
    public string MetadataAddress { get; init; }
    public string ValidIssuer { get; init; }
    public string Audience { get; init; }
}