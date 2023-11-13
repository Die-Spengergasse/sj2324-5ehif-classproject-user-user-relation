using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user.Application.Model

{
    public class Recipe
    {
        [Required] public Key Key { get; }
        [Required] [StringLength(50)] public string Name { get; }

        public Recipe(Key key, string name)
        {
            Key = key;
            Name = name;
        }
    }
}