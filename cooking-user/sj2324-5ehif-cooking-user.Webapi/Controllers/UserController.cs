using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sj2324_5ehif_cooking_user.Application.Infrastructure;
using sj2324_5ehif_cooking_user.Application.Model;
using sj2324_5ehif_cooking_user.Webapi.Services;

namespace sj2324_5ehif_cooking_user.Webapi.Controllers;

public record DeleteUserModel
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}

[ApiController]
[Route("user/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserContext _context;
    private readonly ILogger<UserController> _logger;
    private readonly IPasswordUtils _passwordUtils;

    public UserController(UserContext context, ILogger<UserController> logger, IPasswordUtils passwordUtils)
    {
        _context = context;
        _logger = logger;
        _passwordUtils = passwordUtils;
    }

    [Authorize]
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteUser(DeleteUserModel deleteDto)
    {
        try
        {
            _logger.LogInformation("Attempting to delete user with username: {Username} and email: {Email}",
                deleteDto.Username, deleteDto.Email);

            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Username == deleteDto.Username || u.Email == deleteDto.Email);
            if (user == null)
            {
                _logger.LogWarning("User not found for deletion: {Username}", deleteDto.Username);
                return NotFound("User not found");
            }

            var hashedPassword = _passwordUtils.HashPassword(deleteDto.Password);
            if (user.Password != hashedPassword)
            {
                _logger.LogWarning("Invalid credentials for user deletion: {Username}", deleteDto.Username);
                return Unauthorized("Invalid credentials");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation("User deletion successful for {Username}", deleteDto.Username);
            return Ok("User deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during user deletion");
            return StatusCode(500, "User deletion failed");
        }
    }

    [Authorize]
    [HttpPut("preferences")]
    public async Task<IActionResult> UpdatePreferences([FromBody] List<Preference> preferenceNames)
    {
        try
        {
            var username = HttpContext.User.Identity?.Name;

            var user = await _context.Users.Include(u => u.Preferences)
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                _logger.LogWarning("User not found while updating preferences for: {Username}", username);
                return NotFound("User not found");
            }

            var preferencesToAdd = preferenceNames.Except(user.Preferences).ToList();
            user.AddPreferenceRange(preferencesToAdd);

            var emptyPreferences =
                user.Preferences.Where(p => string.IsNullOrEmpty(p.Name))
                    .ToList(); //check which preferences should be removed
            foreach (var emptyPreference in emptyPreferences) user.RemovePreference(emptyPreference);

            await _context.SaveChangesAsync();

            _logger.LogInformation("User preferences updated for {Username}", username);
            return Ok("User preferences updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during preference update");
            return StatusCode(500, "Preference update failed");
        }
    }
}