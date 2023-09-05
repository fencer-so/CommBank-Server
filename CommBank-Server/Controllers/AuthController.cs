using CommBank.Models;
using CommBank.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommBank.Controllers;

[ApiController]
[Route("api/Auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Post(LoginInput input)
    {
        var user = await _authService.Login(input.Email, input.Password);

        if (user is null)
        {
            return NotFound();
        }

        return NoContent();
    }
}