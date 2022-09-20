using Microsoft.AspNetCore.Mvc;
using TWJobs.Api.Auth.Dtos;
using TWJobs.Api.Auth.Services;

namespace TWJobs.Api.Auth.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var body = await _authService.Register(request);
        return Created("", body);
    }
}
