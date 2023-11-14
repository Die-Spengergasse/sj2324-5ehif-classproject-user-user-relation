using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sj2324_5ehif_cooking_user.Application.Model;

public class Preference
{
    [Key]
    public string Key { get; set; }
    
    [NotMapped]
    public PreferenceKey ProxyKey
    {
        get => new(Key);
        set => Key = value.Value;
    }
    
    [Required(AllowEmptyStrings = false)]
    public string Name { get; }
    
    public Preference(string name)
    {
        ProxyKey = new PreferenceKey();
        Name = name;
    }
    
    protected Preference() { } 
}