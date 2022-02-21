using Blog.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[ApiController]

[Route("")]
public class HomeController : ControllerBase
{
    [HttpGet("")]
    [Authorize]
    public IActionResult Get()
    {
        return Ok("Olá mundo!");
    }

}