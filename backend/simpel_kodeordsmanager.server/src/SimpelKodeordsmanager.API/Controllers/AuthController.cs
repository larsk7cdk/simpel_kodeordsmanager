using Microsoft.AspNetCore.Mvc;
using SimpelKodeordsmanager.Application.Contracts.DTOs.User;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Auth;

namespace SimpelKodeordsmanager.API.Controllers
{
    /// <summary>
    ///    Autentifikation af brugere
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        /// <summary>
        ///     Kontroller om brugeren kan logge ind og returner et JWT token hvis det er tilfældet
        /// </summary>
        [HttpPost, Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDTO request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Email or Password cannot be empty.");
            }

            var user = await authService.LoginAsync(request.Email, request.Password);

            if (!user.IsAuthenticated)
                return Unauthorized(user.Status);

            return Ok(new UserAuthDTO
            {
                Email = user.Email,
                Token = user.JwtToken,
                ExpiresIn = user.ExpiresIn
            });
        }
    }
}