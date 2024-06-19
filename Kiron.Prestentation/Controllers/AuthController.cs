using FluentValidation;
using Kiron.Application.Models;
using Kiron.Application.Services;
using Kiron.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kiron.Presentation.Controllers;

public class AuthController : Controller
{
    private readonly IUserService _userService;
  
    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(User request)
    {
        var result = await _userService.CreateUser(request.Username, request.Password);

        return result.doesUserExits ? Ok(result.messsage) : BadRequest(result.messsage);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(User request)
    {
        var result = await _userService.Login(request.Username, request.Password);

        return result.doesUserExits ? Ok(result.messsage) : Unauthorized(result.messsage);
    }
}
