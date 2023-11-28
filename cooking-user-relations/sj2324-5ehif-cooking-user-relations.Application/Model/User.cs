using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace sj2324_5ehif_cooking_user_relations.Application.Model
{
    public class User : IEntity

    {
        [Key]
        public string Key { get; set; }


        [Required]
        [NotMapped]
        public UserKey ObjectKey
        {
            get => new(Key);
            set => Key = value.Value;
        }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public User(String key, string name)
        {
            Key = key;
            Name = name;
        }


    }
}