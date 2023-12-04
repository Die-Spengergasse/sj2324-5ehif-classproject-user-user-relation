using AutoMapper;
using sj2324_5ehif_cooking_user.Application.DTO;
using sj2324_5ehif_cooking_user.Application.Model;
using DtoMappingProfile = sj2324_5ehif_cooking_user.Application.DTO.DtoMappingProfile;

namespace sj2324_5ehif_cooking_user.Test;
public class DtoTest
{
    private readonly IMapper _mapper;

    public DtoTest()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<DtoMappingProfile>());
        _mapper = config.CreateMapper();
    }
    
    [Fact]
    public void CookbookToCookbookDto()
    {
        var user = new User("ownerUsername", "OwnerLastname", "OwnerFirstname", "owner@example.com", "qwerty12345"){Key = "USR1234567890AB" };
        var cookbook = new Cookbook("USR1234567890AB", "My Cookbook", false) ;
        var recipe = new Recipe("My Recipe","USR1234567890AB");
        cookbook.AddRecipe(recipe);
        cookbook.AddUser(user);

        var cookbookDto = _mapper.Map<CookbookDto>(cookbook);

        Assert.NotNull(cookbookDto);
        Assert.Equal(cookbook.Name, cookbookDto.Name);
        Assert.Equal(cookbook.Private, cookbookDto.Private);
        Assert.Equal(cookbook.Recipes.First().Key, cookbookDto.Recipes.First().Key);
        Assert.Equal(cookbook.Collaborators.First().Key, cookbookDto.Collaborators.First().Key);
        Assert.Equal(cookbook.Key, cookbookDto.Key);
    }

    [Fact]
    public void PreferenceToPreferenceDto()
    {
        var recipe = new Recipe("My Recipe", "USR1234567890AB");

        var recipeDto = _mapper.Map<RecipeDto>(recipe);

        Assert.NotNull(recipeDto);
        Assert.Equal(recipe.Name, recipeDto.Name);
        Assert.Equal(recipe.Key, recipeDto.Key);
    }

    [Fact]
    public void RecipeToRecipeDto()
    {
        var recipe = new Recipe("MyRecipe", "USR1234567890AB") { Key = "REC1234567890AB"};
        var recipeDto = _mapper.Map<RecipeDto>(recipe);
        
        Assert.NotNull(recipeDto);
        Assert.Equal(recipeDto.Name, recipe.Name);
        Assert.Equal(recipeDto.Authorkey, recipe.AuthorKey);
        Assert.Equal(recipeDto.Key, recipe.Key);
    }
    
    [Fact]
    public void UserToUserDto()
    {
        var user = new User("testUser", "Doe", "John", "john.doe@example.com", "qwerty12345");
        var preference = new Preference("Vegan"){Key = "PRE1234567X"};
        user.AddPreference(preference);

        var userDto = _mapper.Map<UserDto>(user);

        Assert.NotNull(userDto);
        Assert.Equal(user.ObjectKey.Value, userDto.Key);
        Assert.Single(userDto.Preferences);
        Assert.Equal(user.Preferences.First().Key, userDto.Preferences.First().Key);
    }
}
