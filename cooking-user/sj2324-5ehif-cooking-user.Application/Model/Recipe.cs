using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sj2324_5ehif_cooking_user.Application.Model;

public class Recipe : IEntity
{
    [Key] public string Key { get; set; }



    [NotMapped]
    public RecipeKey KeyObject
    {
        get => new(Key);
        set => Key = value.Value;
    }

    [Required(AllowEmptyStrings = false)]
    [StringLength(50)]
    public string Name { get; set; }

    public Recipe(string name)
    {
        KeyObject = new RecipeKey();
        Name = name;
    }

    public Recipe(string name, string key)
    {
        Key = key;
        Name = name;
    }

    protected Recipe()
    {
    }
}