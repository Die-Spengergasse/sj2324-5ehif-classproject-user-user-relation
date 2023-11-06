using sj2324_5ehif_cooking_user.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace sj2324_5ehif_cooking_user.Application.DTO
{
    public class CookbookDto
    {
        public long? Id { get; set; }
       // public CookbookKeyDto? CookbookKey { get; set; }
        public UserDto? Owner { get; set; }
        public string? Name { get; set; }
        public bool? Private { get; set; }
        public List<RecipeDto>? Recipes { get; set; } = new();
        public List<UserDto>? Collaborators { get; set; } = new();
    }
}



