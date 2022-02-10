using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.Services;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    /*
    [HttpPost("v1/accounts/")]
    public async Task<IActionResult> Post(
        [FromServices] BlogDataContext context,
        [FromServices] RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest( new ResultViewModel<string>(ModelState.GetErrors()));

        User newUser = new()
        {
            Name = model.Name,
            Email = model.Email,
            Slug = model.Email
                .Replace('@', '-')
                .Replace('.', '-')
                .ToLower()
        };
    }
*/
    
[HttpPost("v1/login")]
    public IActionResult Login([FromServices] TokenService tokenService)
    {
        var token = tokenService.GenerateToken(null);
        return Ok(token);  
    }
}