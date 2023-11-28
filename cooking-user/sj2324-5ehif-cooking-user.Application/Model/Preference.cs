using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sj2324_5ehif_cooking_user.Application.Model;

public class Preference : IEntity
{
    [Key]
    public string Key { get; set; }
    
    [NotMapped]
    public PreferenceKey KeyObject
    {
        get => new(Key);
        set => Key = value.Value;
    }
    
    [Required(AllowEmptyStrings = false)]
    public string Name { get; }
    
    public Preference(string name)
    {
        KeyObject = new PreferenceKey();
        Name = name;
    }
    public Preference(string name,string id)
    {
        Key = id;
        Name = name;
    }
    
    protected Preference() { } 
}