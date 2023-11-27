using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sj2324_5ehif_cooking_user.Application.Model;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace sj2324_5ehif_cooking_user.Application.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CookbookController : ControllerBase
    {
        private readonly YourDbContext _context;

        public CookbookController(YourDbContext context)
        {
            _context = context;
        }

        // POST: api/Cookbook
        [HttpPost]
        public ActionResult<Cookbook> CreateCookbook([FromBody] Cookbook cookbook)
        {
            if (ModelState.IsValid)
            {
                _context.Cookbooks.Add(cookbook);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetCookbook), new { id = cookbook.Id }, cookbook);
            }

            return BadRequest(ModelState);
        }

        // GET: api/Cookbook/user/{userId}
        [HttpGet("user/{userId}")]
        public ActionResult<IEnumerable<Cookbook>> GetCookbooksByUser(long userId)
        {
            var cookbooks = _context.Cookbooks
                .Where(c => c.Owner.Id == userId)
                .ToList();

            return Ok(cookbooks);
        }

        // POST: api/Cookbook/{cookbookId}/AddRecipe
        [HttpPost("{cookbookId}/AddRecipe")]
        public ActionResult<Cookbook> AddRecipeToCookbook(long cookbookId, [FromBody] Recipe recipe)
        {
            var cookbook = _context.Cookbooks
                .Include(c => c.Recipes)
                .FirstOrDefault(c => c.Id == cookbookId);

            if (cookbook != null)
            {
                cookbook.AddRecipe(recipe);
                _context.SaveChanges();

                return Ok(cookbook);
            }

            return NotFound();
        }

        // DELETE: api/Cookbook/{cookbookId}/RemoveRecipe/{recipeId}
        [HttpDelete("{cookbookId}/RemoveRecipe/{recipeId}")]
        public ActionResult<Cookbook> RemoveRecipeFromCookbook(long cookbookId, long recipeId)
        {
            var cookbook = _context.Cookbooks
                .Include(c => c.Recipes)
                .FirstOrDefault(c => c.Id == cookbookId);

            if (cookbook != null)
            {
                var recipeToRemove = cookbook.Recipes.FirstOrDefault(r => r.Id == recipeId);

                if (recipeToRemove != null)
                {
                    cookbook.RemoveRecipe(recipeToRemove);
                    _context.SaveChanges();

                    return Ok(cookbook);
                }
            }

            return NotFound();
        }

        // PUT: api/Cookbook/AddCookbooksToProfile/{profileId}
        [HttpPut("AddCookbooksToProfile/{profileId}")]
        public ActionResult<IEnumerable<Cookbook>> AddCookbooksToProfile(long profileId, [FromBody] List<Cookbook> cookbooks)
        {
            var profile = _context.Profiles
                .Include(p => p.Cookbooks)
                .FirstOrDefault(p => p.Id == profileId);

            if (profile != null)
            {
                profile.Cookbooks.AddRange(cookbooks);
                _context.SaveChanges();

                return Ok(profile.Cookbooks);
            }

            return NotFound();
        }
    }

}
