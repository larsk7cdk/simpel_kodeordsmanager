using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpelKodeordsmanager.API.Controllers.Shared;

namespace SimpelKodeordsmanager.API.Controllers
{
    /// <summary>
    ///    Håndtering af API Sundhedstjek
    /// </summary>
    public class HealthController : AppControllerBase
    {
        /// <summary>
        ///     Sundhedstjek af API'en
        /// </summary>
        [HttpGet, Route("")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GetHealthAsync()
        {
            return await Task.FromResult(Ok("API is healthy"));
        }

        /// <summary>
        ///     Sundhedstjek af API'en med autorisation
        /// </summary>
        [HttpGet, Route("auth")]
        [Authorize]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetHealthAuthAsync()
        {
            return await Task.FromResult(Ok("API is healthy with auth"));
        }
    }
}