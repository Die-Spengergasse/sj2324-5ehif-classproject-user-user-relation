using AutoMapper;
using sj2324_5ehif_cooking_user.Application.Model;

namespace sj2324_5ehif_cooking_user.Application.DTO;

public class DtoMappingProfile : Profile
{
    public DtoMappingProfile()
    {
        // map models to Dtos 
        CreateMap<Cookbook, CookbookDto>();

        CreateMap<Preference, PreferenceDto>();

        CreateMap<Recipe, RecipeDto>();
        
        CreateMap<User, UserDto>();
    }
}