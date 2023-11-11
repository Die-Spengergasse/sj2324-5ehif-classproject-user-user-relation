using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user_relations.Application.Model
{
    public class Recipe
    {
        [Required] public RecipeKey Key { get; }
        [Required][StringLength(50)] public string Name { get; }

        public Recipe(RecipeKey key, string name)
        {
            Key = key;
            Name = name;
        }
    }
}
