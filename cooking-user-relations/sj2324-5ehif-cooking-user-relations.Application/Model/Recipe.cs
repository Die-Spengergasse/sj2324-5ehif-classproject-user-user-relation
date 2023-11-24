using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user_relations.Application.Model
{
    public class Recipe
    {
        public string Id { get; set;}
    
        [NotMapped]
        public RecipeKey ProxyId
        {
            get => new(Id);
            set => Id = value.Value;
        }
        
        [Required(AllowEmptyStrings = false)]
        [StringLength(50)] 
        public string Name { get; set; }
        public Recipe(string name)
        {
            ProxyId = new RecipeKey();
            Name = name;
        }
    
        protected Recipe() { } 
    }
}
