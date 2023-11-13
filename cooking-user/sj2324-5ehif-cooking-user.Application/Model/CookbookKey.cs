namespace sj2324_5ehif_cooking_user.Application.Model;

public class CookbookKey : Key
{
    public CookbookKey() : base(prefix:"COB", length:12)
    {
    }
    public CookbookKey(string value) : base(value:value,prefix:"COB", length:12)
    {
    }
}