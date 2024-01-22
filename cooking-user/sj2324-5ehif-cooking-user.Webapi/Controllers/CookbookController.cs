using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sj2324_5ehif_cooking_user.Application.DTO;
using sj2324_5ehif_cooking_user.Application.Infrastructure;
using sj2324_5ehif_cooking_user.Application.Model;
using sj2324_5ehif_cooking_user.Application.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace sj2324_5ehif_cooking_user.Webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class CookbookController : ControllerBase
{
    private readonly ILogger<CookbookController> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<Cookbook> _cookbookRepository;
    private readonly IRepository<Recipe> _recipeRepository;
    private readonly IRepository<User> _repository;

    private readonly UserContext _context;

    public CookbookController(ILogger<CookbookController> logger, IMapper mapper,
        IRepository<Cookbook> cookbookRepository, IRepository<Recipe> recipeRepository,
        IRepository<User> userRepository, UserContext context)
    {
        _logger = logger;
        _mapper = mapper;
        _cookbookRepository = cookbookRepository;
        _recipeRepository = recipeRepository;
        _repository = userRepository;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCookbook([FromBody] AddCookbookDto addCookbookDto)
    {
        _logger.LogInformation("Creating a new cookbook");
        var cookbook = _mapper.Map<Cookbook>(addCookbookDto);

        try
        {
            _context.Cookbooks.Add(cookbook);
            await _context.SaveChangesAsync();
            return Ok(new { cookbook.Key });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating cookbook");
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpDelete("{key}")]
    public async Task<IActionResult> DeleteCookbook(string key)
    {
        var cookbook = await _context.Cookbooks.FindAsync(key);
        if (cookbook == null)
        {
            _logger.LogWarning($"Cookbook with key {key} not found.");
            return NotFound();
        }

        try
        {
            _context.Cookbooks.Remove(cookbook);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting cookbook with key {key}");
            return StatusCode(500, "Internal Server Error");
        }
    }
    //TODO: Implement in repository that AsyncGetAll returns cookbooks not users
    //[HttpGet("user/{userKey}")]
    //public async Task<IActionResult> GetCookbooksFromUser(string userKey)
    //{
    //    try
    //    {
    //        var (success, message, cookbooks) = await _repository.GetAllAsync();
    //        if (!success)
    //        {
    //            _logger.LogError($"Error retrieving cookbooks: {message}");
    //            return StatusCode(500, "Internal Server Error");
    //        }

    //        var userCookbooks = cookbooks.Where(c => c.OwnerKey == userKey).ToList();

    //        var cookbookDtos = userCookbooks.Select(cb => _mapper.Map<CookbookDto>(cb)).ToList();

    //        return Ok(cookbookDtos);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, $"Error retrieving cookbooks for user {userKey}");
    //        return StatusCode(500, "Internal Server Error");
    //    }
    //}


    [HttpPost("{cookbookKey}/recipe")]
    public async Task<IActionResult> AddRecipeToCookbook(string cookbookKey, [FromBody] RecipeDto recipeDto)
    {
        var (success, message, cookbook) = await _cookbookRepository.GetByIdAsync(cookbookKey);
        if (!success || cookbook == null)
        {
            return NotFound("Cookbook not found.");
        }

        var recipe = _mapper.Map<Recipe>(recipeDto);
        cookbook.AddRecipe(recipe);

        try
        {
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding recipe to cookbook");
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpDelete("{cookbookKey}/recipe/{recipeKey}")]
    public async Task<IActionResult> RemoveRecipeFromCookbook(string cookbookKey, string recipeKey)
    {
        var (success, message, cookbook) = await _cookbookRepository.GetByIdAsync(cookbookKey);
        if (!success || cookbook == null)
        {
            return NotFound("Cookbook not found.");
        }

        var recipe = cookbook.Recipes.FirstOrDefault(r => r.Key == recipeKey);
        if (recipe == null)
        {
            return NotFound("Recipe not found.");
        }

        cookbook.RemoveRecipe(recipe);

        try
        {
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing recipe from cookbook");
            return StatusCode(500, "Internal Server Error");
        }
    }

}
