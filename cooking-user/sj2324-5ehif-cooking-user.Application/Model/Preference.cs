using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper.Configuration.Annotations;

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
    public string Name { get; }
    
    public Preference(string name)
    {
        ProxyKey = new PreferenceKey();
        Name = name;
    }
    
    protected Preference() { } 
}