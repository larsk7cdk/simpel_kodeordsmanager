using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SimpelKodeordsmanager.API.Controllers
{
    /// <summary>
    ///    Håndtering af kodeord og applikationer
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController() : ControllerBase
    {
        /// <summary>
        ///     Sundhedstjek af API'en
        /// </summary>
        [HttpGet, Route("")]
        public async Task<IActionResult> GetHealthAsync()
        {
            return await Task.FromResult(Ok("API is healthy"));
        }

        /// <summary>
        ///     Sundhedstjek af API'en med autorisation
        /// </summary>
        [HttpGet, Route("auth")]
        [Authorize]
        public async Task<IActionResult> GetHealthAuthAsync()
        {
            return await Task.FromResult(Ok("API is healthy with auth"));
        }
    }
}