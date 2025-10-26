namespace SimpelKodeordsmanager.Domain.Entities;

public abstract class BaseEntity
{
    public int Id { get; init; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }
}