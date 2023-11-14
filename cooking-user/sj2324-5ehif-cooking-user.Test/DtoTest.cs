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
    public void AutoMapper_UserDto_MapsCorrectly()
    {
        var userDto = new UserDto
        {
            UserKey = "USR1234567890AB",
            Username = "testUser",
            Lastname = "Doe",
            Firstname = "Jane",
            Email = "jane.doe@example.com",
            Preferences = new List<PreferenceDto>()
            {
                new PreferenceDto { PreferenceKey = "PREF123", Name = "Vegetarian" }
            }
        };

        var user = _mapper.Map<User>(userDto);
        Assert.NotNull(user);
        Assert.Equal(userDto.Username, user.Username);
        Assert.Equal(userDto.Lastname, user.Lastname);
        Assert.Equal(userDto.Firstname, user.Firstname);
        Assert.Equal(userDto.Email, user.Email);
        Assert.Equal(userDto.Preferences.Count, user.Preferences.Count);
    }
    [Fact]
    public void AutoMapper_UserToUserDto_MapsKeyCorrectly()
    {
        var user = new User("testUser", "Doe", "John", "john.doe@example.com");
        user.AddPreference(new Preference(new PreferenceKey("PRE1234567890X"), "Vegan"));

        var userDto = _mapper.Map<UserDto>(user);

        Assert.NotNull(userDto);
        Assert.Equal(user.Key.Value, userDto.UserKey);
        Assert.Single(userDto.Preferences);
        Assert.Equal(user.Preferences.First().Key.Value, userDto.Preferences.First().PreferenceKey);
    }

    [Fact]
    public void AutoMapper_RecipeDto_MapsCorrectly()
    {
        var recipeDto = new RecipeDto
        {
            RecipeKey = "REC1234567890AB",
            Name = "TestRecipe"
        };
        var recipe = _mapper.Map<Recipe>(recipeDto);
        Assert.NotNull(recipe);
        Assert.Equal(recipeDto.RecipeKey, recipe.Key.Value);
        Assert.Equal(recipeDto.Name, recipe.Name);
    }

    [Fact]
    public void AutoMapper_CookbookDto_MapsCorrectly()
    {
        var cookbookDto = new CookbookDto
        {
            CookbookKey = "COB123456789AB",
            Name = "Family Recipes",
            Private = true,
        };

        var cookbook = _mapper.Map<Cookbook>(cookbookDto);
        Assert.NotNull(cookbook);
        Assert.Equal(cookbookDto.Name, cookbook.Name);
        Assert.Equal(cookbookDto.Private, cookbook.Private);
    }

    [Fact]
    public void AutoMapper_PreferenceDto_MapsCorrectly()
    {
        var preferenceDto = new PreferenceDto
        {
            PreferenceKey = "PRE1234567X",
            Name = "Gluten Free"
        };
        var preference = _mapper.Map<Preference>(preferenceDto);
        Assert.NotNull(preference);
        Assert.Equal(preferenceDto.Name, preference.Name);
    }


    [Fact]
    public void AutoMapper_CookbookToCookbookDto_MapsCorrectly()
    {
        var user = new User("ownerUsername", "OwnerLastname", "OwnerFirstname", "owner@example.com");
        var cookbook = new Cookbook(user, "My Cookbook", false);
        cookbook.AddRecipe(new Recipe(new RecipeKey("REC1234567890AB"), "My Recipe"));

        var cookbookDto = _mapper.Map<CookbookDto>(cookbook);

        Assert.NotNull(cookbookDto);
        Assert.Equal(cookbook.Name, cookbookDto.Name);
        Assert.Equal(cookbook.Private, cookbookDto.Private);
        Assert.Equal(cookbook.Recipes.Count, cookbookDto.Recipes.Count);
        Assert.Equal(cookbook.Key.Value, cookbookDto.CookbookKey);
    }

    [Fact]
    public void AutoMapper_RecipeToRecipeDto_MapsCorrectly()
    {
        var recipe = new Recipe(new RecipeKey("REC1234567890AB"), "My Recipe");

        var recipeDto = _mapper.Map<RecipeDto>(recipe);

        Assert.NotNull(recipeDto);
        Assert.Equal(recipe.Name, recipeDto.Name);
        Assert.Equal(recipe.Key.Value, recipeDto.RecipeKey);
    }

    [Fact]
    public void AutoMapper_ApplicationException_ThrownForInvalidRecipeDto()
    {
        var recipeDto = new RecipeDto
        {
            RecipeKey = "REC1234567890AB",
        };

        Assert.Throws<ApplicationException>(() => _mapper.Map<Recipe>(recipeDto));
    }

    [Fact]
    public void AutoMapper_ApplicationException_ThrownForInvalidCookbookDto()
    {
        var cookbookDto = new CookbookDto
        {
            CookbookKey = "COB123456789AB", 
        };

        Assert.Throws<ApplicationException>(() => _mapper.Map<Cookbook>(cookbookDto));
    }
}
