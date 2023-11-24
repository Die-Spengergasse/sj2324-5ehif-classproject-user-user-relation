using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user_relations.Application.Model
{
    public class User
    {
        public string Id { get; set; }

        [Required]
        [NotMapped]
        public UserKey ObjectKey
        {
            get => new(Id);
            set => Id = value.Value;
        }

        public UserKey Key { get; }
        [Required] [StringLength(100)] public string Name { get; }

        public User(UserKey key, string name)
        {
            ObjectKey = key;
            Name = name;
        }
    }
}