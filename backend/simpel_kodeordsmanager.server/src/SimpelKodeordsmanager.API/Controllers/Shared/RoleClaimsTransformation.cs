using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using SimpelKodeordsmanager.API.Models;

namespace SimpelKodeordsmanager.API.Controllers.Shared;

public class RoleClaimsTransformation : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var identity = principal.Identity as ClaimsIdentity;
        
        // when decoding the token with jwt.io roles is present under realm_access
        var realmAccessClaim = identity?.FindFirst("realm_access");

        if (realmAccessClaim is null)
            return Task.FromResult(principal);


        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; // Ignore case when deserializing JSON

        // Deserialize the realm_access JSON to extract the roles
        var realmAccess = JsonSerializer.Deserialize<RealmAccess>(realmAccessClaim.Value, options);

        if (realmAccess?.Roles == null)
            return Task.FromResult(principal);

        foreach (var role in realmAccess.Roles)
        {
            // Add each role as a Claim of type ClaimTypes.Role
            identity!.AddClaim(new Claim(ClaimTypes.Role, role));
        }

        return Task.FromResult(principal);
    }
}