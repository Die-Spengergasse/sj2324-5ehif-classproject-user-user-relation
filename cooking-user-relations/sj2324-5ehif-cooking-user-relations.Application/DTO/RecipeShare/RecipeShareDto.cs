using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sj2324_5ehif_cooking_user_relations.Application.DTO.Recipe;
using sj2324_5ehif_cooking_user_relations.Application.DTO.User;

namespace sj2324_5ehif_cooking_user_relations.Application.DTO.RecipeShare
{
    public class RecipeShareDto
    {
        public string Key { get; set; }
        public RecipeDto Recipe { get; set; }
        public List<UserDto> Collaborators { get; set; } = new List<UserDto>();

    }
}
