using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpelKodeordsmanager.API.Controllers.Shared;
using SimpelKodeordsmanager.Application.Contracts.DTOs.User;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Shared;
using SimpelKodeordsmanager.Domain.Models;

namespace SimpelKodeordsmanager.API.Controllers
{
    /// <summary>
    ///    Brugere håndtering
    /// </summary>
    public class UserController : AppControllerBase
    {
        /// <summary>
        ///     Kontroller om brugeren findes og opret hvis ikke det er tilfældet
        /// </summary>
        [HttpPost, Route("register")]
        [ProducesResponseType(typeof(UserResponseDTO), 201)]
        public async Task<IActionResult> RegisterAsync(
            [FromServices] IRequestHandler<UserRegisterRequestDTO, UserResponseDTO> requestHandler,
            [FromBody] UserRegisterRequestDTO request)
        {
            var result = await requestHandler.InvokeAsync(request);
            return Created(Url.Link("Login", null), result);
        }

        /// <summary>
        ///     Kontroller om brugeren kan logge ind og returner et JWT token hvis det er tilfældet
        /// </summary>
        [HttpPost, Route("login")]
        [ProducesResponseType(typeof(UserResponseDTO), 200)]
        public async Task<IActionResult> LoginAsync(
            [FromServices] IRequestHandler<UserLoginRequestDTO, UserResponseDTO> requestHandler,
            [FromBody] UserLoginRequestDTO request)
        {
            var result = await requestHandler.InvokeAsync(request);
            return Ok(result);
        }


        /// <summary>
        ///     Liste af alle brugere der er oprettet
        /// </summary>
        [HttpGet, Route("users")]
        [Authorize(Policy = Role.Admin)]
        [ProducesResponseType(typeof(IReadOnlyList<UserDetailsResponseDTO>), 200)]
        public async Task<IActionResult> LoginAsync(
            [FromServices] IResponseHandler<IReadOnlyList<UserDetailsResponseDTO>> responseHandler)
        {
            var result = await responseHandler.InvokeAsync();
            return Ok(result);
        }
    }
}