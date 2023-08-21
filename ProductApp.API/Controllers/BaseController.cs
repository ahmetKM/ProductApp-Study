using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApp.API.Filters;
using ProductApp.Core.DTOs;

namespace ProductApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            return new ObjectResult(response.StatusCode == 204 ? null : response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
