using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sj2324_5ehif_cooking_user.Application.Model;

public class Preference : IEntity
{
    [Key] public long Id { get; private set; }
    public string Key { get; set; }
    [Required(AllowEmptyStrings = false)] public string Name { get; set; }

    public Preference(string name)
    {
        KeyObject = new PreferenceKey();
        Name = name;
    }

    protected Preference()
    {
    }

    [NotMapped]
    public PreferenceKey KeyObject
    {
        get => new(Key);
        set => Key = value.Value;
    }
}