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
    public void AutoMapper_UserDtor_MapsCorrectly()
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
    public void AutoMapper_ShouldThrowException_WhenPreferenceDtoNameIsInvalid()
    {
        var invalidPreferenceDto = new PreferenceDto
        {
            PreferenceKey = "PRE1234567X", 
            Name = "" 
        };

        var exception = Assert.Throws<ApplicationException>(() =>
            _mapper.Map<Preference>(invalidPreferenceDto)
        );

        Assert.Equal("Invalid preference key.", exception.Message);
    }

    [Fact]
    public void AutoMapper_ShouldThrowException_WhenPreferenceDtoKeyIsInvalid()
    {
        var invalidPreferenceDto = new PreferenceDto
        {
            PreferenceKey = "",
            Name = "Vegan"
        };

        var exception = Assert.Throws<ApplicationException>(() =>
            _mapper.Map<Preference>(invalidPreferenceDto)
        );

        Assert.Equal("Invalid preference name.", exception.Message);
    }
}
