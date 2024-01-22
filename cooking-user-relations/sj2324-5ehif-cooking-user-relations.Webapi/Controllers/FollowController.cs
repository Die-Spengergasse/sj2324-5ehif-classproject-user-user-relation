using Microsoft.AspNetCore.Mvc;
using sj2324_5ehif_cooking_user_relations.Application.DTO.Follow;
using sj2324_5ehif_cooking_user_relations.Application.Services;
using System;

namespace sj2324_5ehif_cooking_user_relations.Webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class FollowController : ControllerBase
{
    private readonly IFollowService _followService;
    
    public FollowController(IFollowService followService)
    {
        _followService = followService;     
    }

    [HttpPost]
    public async Task<IActionResult> AddFollower([FromBody] AddFollowDto addFollowDto)
    {
        try
        {
            var success = await _followService.AddFollower(addFollowDto);

            if (success)
            {
                return Ok("Follower added successfully.");
            }      
            return BadRequest("Failed to add follower.");
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }
    
    [HttpDelete("{key}")]
    public async Task<IActionResult> DeleteFollower(string key)
    {
        try
        {
            var success = await _followService.DeleteFollower(key);

            if (success)
            {
                return Ok("Follower deleted successfully.");
            }      
            return BadRequest("Failed to delete follower.");
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var followerDTOs = await _followService.GetAllFollowsAsync();
            return Ok(followerDTOs);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }
}