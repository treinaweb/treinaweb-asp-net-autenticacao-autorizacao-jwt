using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TWJobs.Api.Users.Dtos;
using TWJobs.Api.Users.Services;

namespace TWJobs.Api.Users.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUserRequest request)
    {
        var user = await _userService.CreateAsync(request);
        return Created("", user);
    }
}