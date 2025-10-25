using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace SimpelKodeordsmanager.API.Controllers.Shared
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public abstract class AppControllerBase : ControllerBase
    {
    }
}