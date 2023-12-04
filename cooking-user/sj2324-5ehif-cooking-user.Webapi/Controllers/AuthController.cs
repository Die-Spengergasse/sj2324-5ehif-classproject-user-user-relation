using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sj2324_5ehif_cooking_user.Application.DTO;
using sj2324_5ehif_cooking_user.Application.Model;
using sj2324_5ehif_cooking_user.Application.Repository;
using sj2324_5ehif_cooking_user.Webapi.Services;

namespace sj2324_5ehif_cooking_user.Webapi.Controllers;

[ApiController]
[Route("auth/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtUtils _jwtUtils;
    private readonly ILogger<AuthController> _logger;
    private readonly IRepository<User> _repository;

    public AuthController(ILogger<AuthController> logger, JwtUtils jwtUtils,
        IRepository<User> repository)
    {
        _logger = logger;
        _jwtUtils = jwtUtils;
        _repository = repository;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("registration")]
    public async Task<IActionResult> Register([FromBody] RegisterModel userDto)
    {
        _logger.LogInformation("Attempting user registration for {Username} with email {Email}", userDto.Username,
            userDto.Email);

        var existingUser = (await _repository.GetAllAsync()).entity.SingleOrDefault(
            u => u.Username == userDto.Username && u.Email == userDto.Email);
        if (existingUser is not null)
        {
            _logger.LogWarning("Username or Email already exists");
            return Conflict("Username or Email already exists");
        }

        var hashedPassword = new PasswordUtils().HashPassword(userDto.Password);

        await _repository.InsertOneAsync(new User(userDto.Username, userDto.Lastname, userDto.Firstname, userDto.Email,
            hashedPassword));

        _logger.LogInformation("User registration successful for {Username}", userDto.Username);
        return Ok("Registration successful");
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginDto)
    {
        _logger.LogInformation("Attempting user login for {Username}", loginDto.Username);

        var user = (await _repository.GetAllAsync()).entity.SingleOrDefault(u => u.Username == loginDto.Username);
        if (user == null)
        {
            _logger.LogWarning("User not found for login: {Username}", loginDto.Username);
            return NotFound("User not found");
        }

        var hashedPassword = new PasswordUtils().HashPassword(loginDto.Password);
        if (user.Password != hashedPassword)
        {
            _logger.LogWarning("Invalid credentials for user: {Username}", loginDto.Username);
            return Unauthorized("Invalid credentials");
        }

        var token = _jwtUtils.GenerateJwtToken(user.Key);
        _logger.LogInformation("User login successful for {Username}", loginDto.Username);
        return Ok(new { token });
    }
}