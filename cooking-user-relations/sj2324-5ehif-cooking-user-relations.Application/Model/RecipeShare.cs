using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user_relations.Application.Model
{
    public class RecipeShare :IEntity
    {
        private List<User> _collaborators = new();

        [Key] public long Id { get; private set; }
        [Required] public string Key { get; set; }
        
        [NotMapped]
        public RecipeShareKey ObjectKey
        {
            get => new(Key);
            set => Key = value.Value;
        }

        public virtual IReadOnlyCollection<User> Collaborators => _collaborators;
        [Required] public Recipe Recipe { get; }

#pragma warning disable CS8618
        protected RecipeShare()
        {
        }
#pragma warning restore CS8618

        public RecipeShare(Recipe recipe, List<User> collaborators)
        {
            Recipe = recipe ?? throw new ArgumentNullException(nameof(recipe), "Recipe cannot be null.");

            if (collaborators == null || collaborators.Contains(null))
            {
                throw new ArgumentException("Collaborators list cannot be null or contain null entries.", nameof(collaborators));
            }

            // Optional: Check for duplicate collaborators, if that's important for your domain
            var uniqueCollaborators = collaborators.Distinct().ToList();
            if (uniqueCollaborators.Count != collaborators.Count)
            {
                throw new ArgumentException("Collaborators list cannot contain duplicates.", nameof(collaborators));
            }

            ObjectKey = new RecipeShareKey();
            Recipe = recipe;
            _collaborators = uniqueCollaborators;
        }

        public void AddCollaborater(User collaborator)
        {
            _collaborators.Add(collaborator);
        }

        public void AddCollaboraterRange(List<User> collaborators)
        {
            _collaborators.AddRange(collaborators);
        }

        public void RemoveCollaborator(User collaborator)
        {
            _collaborators.Remove(collaborator);
        }

        public void ClearCollaborators()
        {
            _collaborators.Clear();
        }

    }
}
