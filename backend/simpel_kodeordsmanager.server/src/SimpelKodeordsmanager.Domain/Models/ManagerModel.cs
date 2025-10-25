namespace SimpelKodeordsmanager.Domain.Models;

public class ManagerModel
{
    public required string Email { get; init; }
    public List<ManagerApplicationModel> ManagerApplications { get; init; } = [];
}