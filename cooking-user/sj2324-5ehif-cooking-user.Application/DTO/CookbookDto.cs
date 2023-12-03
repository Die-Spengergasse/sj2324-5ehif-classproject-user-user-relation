namespace sj2324_5ehif_cooking_user.Application.DTO;

public class CookbookDto
{
    public string CookbookKey { get; set; }
    public UserDto Owner { get; set; }
    public string Name { get; set; }
    public bool Private { get; set; }
    public List<RecipeDto> Recipes { get; set; } = new();
    public List<UserDto> Collaborators { get; set; } = new();
}