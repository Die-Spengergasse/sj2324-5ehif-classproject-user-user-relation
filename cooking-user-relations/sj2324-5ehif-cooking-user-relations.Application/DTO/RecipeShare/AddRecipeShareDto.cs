using sj2324_5ehif_cooking_user_relations.Application.DTO.Recipe;
using sj2324_5ehif_cooking_user_relations.Application.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user_relations.Application.DTO.RecipeShare
{
    public class AddRecipeShareDto
    {
        public RecipeDto Recipe { get; set; }
        public List<UserDto> Collaborators { get; set; } = new List<UserDto>();
    }
}
