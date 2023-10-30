using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user.Application.Model
{
    public class Cookbook
    {
        private List<Recipe> _recipes = new();
        private List<User> _collaborators = new();
#pragma warning disable CS8618
        protected Cookbook()
        {
        }
#pragma warning restore CS8618
        public Cookbook(User owner, string name, bool @private)
        {
            Owner = owner;
            Name = name;
            Private = @private;
            Key = new CookbookKey();
        }

        public long Id { get; private set; }

        [Required] public CookbookKey Key { get; }
        [Required] public User Owner { get; set; }
        [Required] [StringLength(100)] public string Name { get; set; }
        public bool Private { get; set; }

        public virtual IReadOnlyCollection<Recipe> Recipes => _recipes;
        public virtual IReadOnlyCollection<User> Collaborators => _collaborators;

        public void AddRecipe(Recipe recipe)
        {
            _recipes.Add(recipe);
        }

        public void ClearRecipe()
        {
            _recipes.Clear();
        }

        public void RemoveRecipe(Recipe recipe)
        {
            _recipes.Remove(recipe);
        }

        public void AddRecipeRange(List<Recipe> recipes)
        {
            foreach (var recipe in recipes)
            {
                AddRecipe(recipe);
            }
        }

        public void AddUser(User user)
        {
            _collaborators.Add(user);
        }

        public void ClearUser()
        {
            _collaborators.Clear();
        }

        public void RemoveUser(User user)
        {
            _collaborators.Remove(user);
        }

        public void AddUserRange(List<User> users)
        {
            foreach (var user in users)
            {
                AddUser(user);
            }
        }
    }
}