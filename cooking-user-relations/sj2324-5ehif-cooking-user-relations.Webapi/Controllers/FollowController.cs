using Microsoft.AspNetCore.Mvc;
using sj2324_5ehif_cooking_user_relations.Application.DTO.Follow;
using sj2324_5ehif_cooking_user_relations.Application.Services;

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
        var success = await _followService.AddFollower(addFollowDto);

        if (success)
        {
            return Ok("Follower added successfully.");
        }      
        return BadRequest("Failed to add follower.");
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var followerDTOs = await _followService.GetAllFollowsAsync();
        return Ok(followerDTOs);
    }
}
