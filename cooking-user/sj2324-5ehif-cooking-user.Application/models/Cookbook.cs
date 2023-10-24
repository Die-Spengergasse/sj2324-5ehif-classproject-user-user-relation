using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user.Application.models
{
    public class Cookbook
    {
        public long Id { get; set; }
        //TODO
        [Required]
        public string Key { get; set; }
        public List<Recipe> Recipes { get; set; }
        [Required]
        public User Owner { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public bool Private { get; set; }
        public List<User> Collaborators { get; set; }
    }
}
