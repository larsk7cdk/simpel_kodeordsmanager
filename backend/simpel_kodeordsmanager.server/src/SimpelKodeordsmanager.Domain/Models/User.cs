﻿using SimpelKodeordsmanager.Domain.Entities;

namespace SimpelKodeordsmanager.Domain.Models;

public class User : BaseEntity
{
    public required string Email { get; init; }
    public required string Name { get; init; }
    public required string Password { get; init; }
}