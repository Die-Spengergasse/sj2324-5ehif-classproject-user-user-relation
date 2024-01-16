using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sj2324_5ehif_cooking_user.Application.Model;

public class Recipe : IEntity
{
    [Key] public string Key { get; set; }
    
    public string Name { get; set; }

    public string AuthorKey { get; set; }

    public Recipe(string name, string authorKey)
    {
        KeyObject = new RecipeKey();
        Name = name;
        AuthorKey = authorKey;
    }

    protected Recipe()
    {
    }

    [NotMapped]
    public RecipeKey KeyObject
    {
        get => new(Key);
        set => Key = value.Value;
    }
}