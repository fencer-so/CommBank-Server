using CommBank_Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommBank_Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InitController : ControllerBase
{
    private readonly IInitService _initService;

    public InitController(IInitService initService)
    {
        _initService = initService;
    }

    [HttpGet]
    public async Task Init()
    {
        await _initService.Init();
    }
}