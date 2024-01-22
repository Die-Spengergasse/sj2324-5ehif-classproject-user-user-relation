using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sj2324_5ehif_cooking_user_relations.Application.DTO.User;
using sj2324_5ehif_cooking_user_relations.Application.Model;
using sj2324_5ehif_cooking_user_relations.Application.Repository;

namespace sj2324_5ehif_cooking_user_relations.Webapi.Controllers;
/// <summary>
/// The update/deleate/create actions are only to be used by the intercommunication-service !!!!!!
/// otherwise there will be dire consequences!
/// </summary>
/// <param name="key"></param>
/// <param name="username"></param>


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
        if (!Key.CheckKey(userDto.Key))
        {
            return Conflict("Key is in wrong format");
        }

        var existingUser = (await _repository.GetAllAsync()).entity.SingleOrDefault(
            u => u.Name == userDto.Name);
        if (existingUser is not null)
        {
            return Conflict("Username exists");
        }
        await _repository.InsertOneAsync(new User(userDto.Key, userDto.Name));
 
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