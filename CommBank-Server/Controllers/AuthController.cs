using Microsoft.AspNetCore.Mvc;
using CommBank.Services;
using CommBank.Models;

namespace CommBank.Controllers;

[ApiController]
[Route("api/Auth")]
<<<<<<< HEAD
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService) =>
        _authService = authService;
=======
public class AuthController(AuthService authService) : ControllerBase
{
    private readonly AuthService _authService = authService;
>>>>>>> 2bc1eb6 (Your commit message)

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