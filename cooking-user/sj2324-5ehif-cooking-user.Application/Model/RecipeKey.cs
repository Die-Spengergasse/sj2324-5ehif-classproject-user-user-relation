namespace sj2324_5ehif_cooking_user.Application.Model;

public class RecipeKey : Key
{
    public RecipeKey() : base(prefix:"REC", length:15)
    {
    }
    
    public RecipeKey(string value) : base(value:value,prefix:"REC", length:15)
    {
    }
}