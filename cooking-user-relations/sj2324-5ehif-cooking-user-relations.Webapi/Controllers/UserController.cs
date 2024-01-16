using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sj2324_5ehif_cooking_user_relations.Application.Infrastructure;
using sj2324_5ehif_cooking_user_relations.Application.Model;
using sj2324_5ehif_cooking_user_relations.Application.Repository;

namespace sj2324_5ehif_cooking_user_relations.Webapi.Controllers;
/// <summary>
/// The update/deleate/create actions are only to be used by the intercommunicatio-service !!!!!!
/// otherwise there will be dire consequences!
/// </summary>
/// <param name="key"></param>
/// <param name="username"></param>
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
    public async Task<IActionResult> AddUser([FromBodx`y] UserDto userDto)
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
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = (await _repository.GetAllAsync()).entity;
        return Ok(users);
    }
    [HttpDelete("{key}")]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteUser(string key)
    {
        
        if (!Key.CheckKey(key))
        {
            return Conflict("Key is in wrong format");
        }

        var existingUser = (await _repository.GetAllAsync()).entity.SingleOrDefault(
            u => u.Key == key);
        if (existingUser is null)
        {
            return Conflict("Username does not exist");
        }

        await _repository.DeleteOneAsync(existingUser.Key);
        Console.WriteLine($"deleted: key: {key}");

        return NoContent();
    }
}