using Todo.Api.Auth;
using Todo.Domain.DTO.User;
using Todo.Domain.Models;
using Todo.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly JWTokenGenerator _jwtGenerator;
    private readonly IAuthService _authService;
    private readonly int _expirationMinutes;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly string _key;

    public AuthController(IConfiguration configuration, IAuthService authService)
    {
        _config = configuration;
        _authService = authService;
        _key = _config.GetValue<string>("Jwt:Key")!;
        _expirationMinutes = _config.GetValue<int>("Jwt:ExpireMinutes");
        _issuer = _config.GetValue<string>("Jwt:Issuer")!;
        _audience = _config.GetValue<string>("Jwt:Audience")!;

        _jwtGenerator = new JWTokenGenerator();
    }

    [HttpPost]
    [Route("[action]")]
    [AccessibleBy(AccessFlags.Everyone)]
    public async Task<ActionResult> Register(UserRegistrationDTO user)
    {
        if (user == null || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName))
        {
            return BadRequest("Enter all User Infos");
        }

        User? registeredUser = await _authService.RegisterAsync(user);

        return registeredUser != null
            ? Ok(_jwtGenerator.GenerateToken(registeredUser, _key, _expirationMinutes, _issuer, _audience))
            : BadRequest("Email already Exists");
    }

    [HttpPost]
    [Route("[action]")]
    [AccessibleBy(AccessFlags.Everyone)]
    public async Task<ActionResult> Login(UserLoginDTO user)
    {
        if (user == null || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
        {
            return BadRequest("Enter all User Infos");
        }

        User? loggedInUser = await _authService.LoginAsync(user);

        return loggedInUser != null
            ? Ok(_jwtGenerator.GenerateToken(loggedInUser, _key, _expirationMinutes, _issuer, _audience))
            : BadRequest("Username or Passwort are wrong");
    }
}
