using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sj2324_5ehif_cooking_user.Application.DTO;
using sj2324_5ehif_cooking_user.Application.Infrastructure;
using sj2324_5ehif_cooking_user.Application.Model;
using sj2324_5ehif_cooking_user.Webapi.Services;

namespace sj2324_5ehif_cooking_user.Webapi.Controllers;

[ApiController]
[Route("auth/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserContext _context;
    private readonly JwtUtils _jwtUtils;
    private readonly ILogger<AuthController> _logger;
    
    public AuthController(UserContext context, ILogger<AuthController> logger, JwtUtils jwtUtils)
    {
        _context = context;
        _logger = logger;
        _jwtUtils = jwtUtils;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("registration")]
    public async Task<IActionResult> Register(RegisterModel userDto)
    {
        try
        {
            _logger.LogInformation("Attempting user registration for {Username} with email {Email}", userDto.Username,
                userDto.Email);

            var existingUser =
                await _context.Users.FirstOrDefaultAsync(
                    u => u.Username == userDto.Username && u.Email == userDto.Email);
            if (existingUser != null)
            {
                _logger.LogWarning("Username or Email already exists");
                return Conflict("Username or Email already exists");
            }

            var hashedPassword = new PasswordUtils().HashPassword(userDto.Password);
            var newUser = new User(userDto.Username, userDto.Lastname, userDto.Firstname, userDto.Email,
                hashedPassword);

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            _logger.LogInformation("User registration successful for {Username}", userDto.Username);
            return Ok("Registration successful");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during registration");
            return StatusCode(500, "Registration failed");
        }
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginModel loginDto)
    {
        try
        {
            _logger.LogInformation("Attempting user login for {Username}", loginDto.Username);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username);
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

            var token = _jwtUtils.GenerateJwtToken(loginDto.Username);
            _logger.LogInformation("User login successful for {Username}", loginDto.Username);
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during login");
            return StatusCode(500, "Login failed");
        }
    }
}