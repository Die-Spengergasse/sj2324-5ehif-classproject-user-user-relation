namespace sj2324_5ehif_cooking_user.Application.Model;

public class PreferenceKey : Key
{
    public PreferenceKey() : base(prefix: "PRE", length:10)
    {
    }
    
    public PreferenceKey(string value) : base(value:value,prefix: "PRE", length:10)
    {
    }
}