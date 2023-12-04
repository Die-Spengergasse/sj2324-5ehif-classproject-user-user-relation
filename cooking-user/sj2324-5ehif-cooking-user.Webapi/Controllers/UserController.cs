using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sj2324_5ehif_cooking_user.Application.DTO;
using sj2324_5ehif_cooking_user.Application.Model;
using sj2324_5ehif_cooking_user.Application.Repository;
using sj2324_5ehif_cooking_user.Webapi.Services;

namespace sj2324_5ehif_cooking_user.Webapi.Controllers;

[ApiController]
[Route("user/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<User> _repository;

    public UserController(ILogger<UserController> logger,
        IMapper mapper, IRepository<User> repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    [Authorize]
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteUser()
    {
        var userKey = HttpContext.User.Identity?.Name;
        _logger.LogInformation("Attempting to delete user with key: {Key}",
            userKey);

        if (string.IsNullOrEmpty(userKey))
        {
            _logger.LogError("User key: {Key} not found in claims.", userKey);
            return BadRequest("User key not found.");
        }

        await _repository.DeleteOneAsync(userKey);

        _logger.LogInformation("User deletion successful for {Key}", userKey);
        return Ok("User deleted successfully");
    }

    [Authorize]
    [HttpPut("preferences")]
    public async Task<IActionResult> UpdatePreferences([FromBody] List<PreferenceDto> preferences)
    {
        var key = HttpContext.User.Identity?.Name;

        var user = (await _repository.GetAllAsync()).entity.SingleOrDefault(u => u.Key == key);

        if (user is null)
        {
            _logger.LogWarning("User not found while updating preferences for: {key}", key);
            return NotFound("User not found");
        }

        var preferencesToAdd = preferences
            .Select(dto => _mapper.Map<Preference>(dto))
            .Except(user.Preferences)
            .ToList();

        user.AddPreferenceRange(preferencesToAdd);

        var emptyPreferences =
            user.Preferences.Where(p => string.IsNullOrEmpty(p.Name))
                .ToList(); //check which preferences should be removed
        foreach (var emptyPreference in emptyPreferences) user.RemovePreference(emptyPreference);

        await _repository.SaveChangesAsync();

        _logger.LogInformation("User preferences updated for {key}", key);
        return Ok("User preferences updated successfully");
    }

    [Authorize]
    [HttpPut("changePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] string currentPassword, string newPassword)
    {
        var key = HttpContext.User.Identity?.Name;

        var user = (await _repository.GetAllAsync()).entity.SingleOrDefault(u => u.Key == key);

        if (user is null)
        {
            _logger.LogWarning("User not found while changing password for: {key}", key);
            return NotFound("User not found");
        }

        var hashedCurrentPassword = new PasswordUtils().HashPassword(currentPassword);

        if (!user.Password.Equals(hashedCurrentPassword))
        {
            _logger.LogWarning("Invalid current password");
            return Unauthorized("Invalid current password");
        }

        var hashedNewPassword = new PasswordUtils().HashPassword(newPassword);
        user.Password = hashedNewPassword;
        await _repository.SaveChangesAsync();

        _logger.LogInformation("Password changed successfully for {key}", key);
        return Ok("Password changed successfully");
    }

    [Authorize]
    [HttpPut("updateProfile")]
    public async Task<IActionResult> UpdateProfile([FromBody] RegisterModel updatedUser)
    {
        var key = HttpContext.User.Identity?.Name;

        var user = (await _repository.GetAllAsync()).entity.SingleOrDefault(u => u.Key == key);

        if (user is null)
        {
            _logger.LogWarning("User not found while updating profile for: {key}", key);
            return NotFound("User not found");
        }

        user.Username = updatedUser.Username ?? user.Username;
        user.Lastname = updatedUser.Lastname ?? user.Lastname;
        user.Firstname = updatedUser.Firstname ?? user.Firstname;
        user.Email = updatedUser.Email ?? user.Email;
        //do not update password here, for "security"/good practice purposes

        await _repository.SaveChangesAsync();

        _logger.LogInformation("Profile updated successfully for {key}", key);
        return Ok("Profile updated successfully");
    }
}