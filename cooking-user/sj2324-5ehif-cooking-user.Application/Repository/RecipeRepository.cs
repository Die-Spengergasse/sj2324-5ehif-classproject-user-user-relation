using Microsoft.EntityFrameworkCore;
using sj2324_5ehif_cooking_user.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user.Application.Repository
{
     public class RecipeRepository : Repository<Recipe>

    {

        public RecipeRepository(DbContext context) : base(context)

        {

        }

    }
}
