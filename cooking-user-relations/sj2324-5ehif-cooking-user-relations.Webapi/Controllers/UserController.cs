using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sj2324_5ehif_cooking_user_relations.Application.Infrastructure;
using sj2324_5ehif_cooking_user_relations.Application.Model;
using sj2324_5ehif_cooking_user_relations.Application.Repository;

namespace sj2324_5ehif_cooking_user_relations.Webapi.Controllers;

public record UserDto(string key, string username);

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IRepository<User> _repository;

    public UserController(IRepository<User> repository)
    {
        _repository = repository;
    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
    {
        if (!Key.CheckKey(userDto.key))
        {
            return Conflict("Key is in wrong format");
        }

        var existingUser = (await _repository.GetAllAsync()).entity.SingleOrDefault(
            u => u.Name == userDto.username);
        if (existingUser is not null)
        {
            return Conflict("Username exists");
        }


        await _repository.InsertOneAsync(new User(userDto.key, userDto.username));
        return Ok(userDto);
    }
}