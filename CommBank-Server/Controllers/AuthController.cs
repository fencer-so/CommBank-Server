using Microsoft.AspNetCore.Mvc;
using CommBank.Services;
using CommBank.Models;

namespace CommBank.Controllers;

[ApiController]
[Route("api/Auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) =>
        _authService = authService;

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