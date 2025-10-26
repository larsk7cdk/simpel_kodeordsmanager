using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpelKodeordsmanager.API.Controllers.Shared;
using SimpelKodeordsmanager.Application.Contracts.DTOs.Manager;
using SimpelKodeordsmanager.Application.Contracts.Interfaces.Shared;
using SimpelKodeordsmanager.Domain.Models;

namespace SimpelKodeordsmanager.API.Controllers
{
    /// <summary>
    ///    Håndtering af kodeord
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Role.Member)]
    public class ManagerController : AppControllerBase
    {
        /// <summary>
        ///     Opret nyt kodeord
        /// </summary>
        [HttpPost, Route("create")]
        [ProducesResponseType(201)]
        public async Task<IActionResult> CreateAsync(
            [FromServices] IRequestHandler<ManagerCreateRequestDTO> requestHandler,
            [FromBody] ManagerCreateRequestDTO request)
        {
            await requestHandler.InvokeAsync(request);
            return Created();
        }

        /// <summary>
        ///     Hsnt alle kodeord for en bruger
        /// </summary>
        [HttpGet, Route("getall")]
        [ProducesResponseType(typeof(IReadOnlyList<ManagerResponseDTO>), 200)]
        public async Task<IActionResult> GetAllAsync(
            [FromServices] IRequestHandler<ManagerReadRequestDTO, IReadOnlyList<ManagerResponseDTO>> requestHandler,
            [FromBody] ManagerReadRequestDTO request)
        {
            var passwordList = await requestHandler.InvokeAsync(request);
            return Ok(passwordList);
        }
    }
}