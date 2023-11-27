using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user_relations.Application.DTO
{
    public class RecipeShareDto
    {
        public string RecipeShareKey { get; set; } 
        public RecipeDto Recipe { get; set; } 
        public List<UserDto> Collaborators { get; set; } = new List<UserDto>();

    }
}
