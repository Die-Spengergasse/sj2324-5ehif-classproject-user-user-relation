using Microsoft.EntityFrameworkCore;
using sj2324_5ehif_cooking_user.Application.Model;

namespace sj2324_5ehif_cooking_user.Application.Repository;

public class RecipeRepository : Repository<Recipe>
{
    public RecipeRepository(DbContext context) : base(context)
    {
    }
}