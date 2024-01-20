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
        CreateMap<PreferenceDto, Preference>().
            ConstructUsing(dto => new Preference(dto.Name));

        CreateMap<Recipe, RecipeDto>();
        
        CreateMap<User, UserDto>();
    }
}