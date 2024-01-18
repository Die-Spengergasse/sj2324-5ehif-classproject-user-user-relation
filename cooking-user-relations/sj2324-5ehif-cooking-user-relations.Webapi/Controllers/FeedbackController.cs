using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sj2324_5ehif_cooking_user_relations.Application.DTO.Feedback;
using sj2324_5ehif_cooking_user_relations.Application.Model;
using sj2324_5ehif_cooking_user_relations.Application.Repository;

namespace sj2324_5ehif_cooking_user_relations.Webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class FeedbackController : ControllerBase
{
    
    private readonly IRepository<Feedback> _repository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Recipe> _recipeRepository;
    private readonly HttpContext _httpContext;
    
    public FeedbackController(IRepository<Feedback> repository, IRepository<User> userRepository, IRepository<Recipe> recipeRepository, HttpContext httpContext)
    {
        _repository = repository;
        _userRepository = userRepository;
        _recipeRepository = recipeRepository;
        _httpContext = httpContext;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddFeedback([FromBody] AddFeedbackDto addFeedbackDto)
    {
        // Get logged in user to use as "From".
        var fromUserKey = _httpContext.User.Identity?.Name;
        if (string.IsNullOrEmpty(fromUserKey)) return Unauthorized();
        
        var fromUser = await _userRepository.GetByIdAsync(fromUserKey);
        if (!fromUser.success) return BadRequest("Logged in user doesnt exist");
        
        // Get recipe
        var recipe = await _recipeRepository.GetByIdAsync(addFeedbackDto.Recipe.Key);
        if (!recipe.success) return BadRequest();
        
        // Get to user
        var toUser = await _userRepository.GetByIdAsync(recipe.entity.AuthorKey);
        if (!toUser.success) return BadRequest();
        
        var exists = await _repository.GetAsync(f => f.From == fromUser.entity && f.To == toUser.entity && f.Recipe == recipe.entity);
        if (exists.success) return BadRequest("Already Exists");

        var toSave = new Feedback(fromUser.entity, toUser.entity, addFeedbackDto.Rating,
            recipe.entity, addFeedbackDto.Text);
        
        var result = await _repository.InsertOneAsync(toSave);
        if (result.success) return CreatedAtAction(nameof(GetFeedbackById), new {id = toSave.Key}, toSave);

        return BadRequest();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFeedbackById(string id)
    {
        var result = await _repository.GetByIdAsync(id);
        if (result.success) return Ok(result.entity);
        return BadRequest();
    }
    
    [HttpGet("recipe/{id}")]
    public async Task<IActionResult> GetFeedbackByRecipeId(string id)
    {
        var result = await _repository.GetAsync(f => f.Recipe.Key == id);
        if (result.success) return Ok(result.entity);
        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _repository.GetAllAsync();
        if (result.success) return Ok(result.entity);

        return BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFeedback([FromBody] UpdateFeedbackDto feedbackDto, string id)
    {
        var feedback = await _repository.GetByIdAsync(id);
        if (!feedback.success) return BadRequest();

        feedback.entity.Text = feedbackDto.Text;
        feedback.entity.Rating = feedbackDto.Rating;
        
        var result = await _repository.UpdateOneAsync(feedback.entity);
        if (result.success) return Ok();
        
        return BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFeedback(string id)
    {
        var result = await _repository.DeleteOneAsync(id);
        if (result.success) return Ok();
        return BadRequest();
    }
    
}