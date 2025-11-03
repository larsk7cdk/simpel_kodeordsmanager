namespace SimpelKodeordsmanager.API.Models;

public class RealmAccess
{
    public List<string>? Roles { get; init; } // one user can be assigned multiple roles
}