using AutoMapper;
using sj2324_5ehif_cooking_user.Application.DTO;
using sj2324_5ehif_cooking_user.Application.Model;
class DtoMappingProfile : Profile
{
    public DtoMappingProfile()
    {
        // Mapping for User and UserDto
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>()
            .BeforeMap((src, dst) =>
            {
                if (string.IsNullOrEmpty(src.Username)) { throw new ApplicationException("Invalid username."); }
                if (string.IsNullOrEmpty(src.Lastname)) { throw new ApplicationException("Invalid lastname."); }
                if (string.IsNullOrEmpty(src.Firstname)) { throw new ApplicationException("Invalid firstname."); }
                if (string.IsNullOrEmpty(src.Email)) { throw new ApplicationException("Invalid email."); }
            });

        // Mapping for Cookbook and CookbookDto
        CreateMap<Cookbook, CookbookDto>();
        CreateMap<CookbookDto, Cookbook>()
            .BeforeMap((src, dst) =>
            {
                if (string.IsNullOrEmpty(src.Name)) { throw new ApplicationException("Invalid cookbook name."); }
            });

        // Mapping for Recipe and RecipeDto
        CreateMap<Recipe, RecipeDto>();
        CreateMap<RecipeDto, Recipe>()
            .BeforeMap((src, dst) =>
            {
                if (string.IsNullOrEmpty(src.Name)) { throw new ApplicationException("Invalid recipe name."); }
            });

        // Mapping for Preference and PreferenceDto
        CreateMap<Preference, PreferenceDto>();
        CreateMap<PreferenceDto, Preference>()
            .BeforeMap((src, dst) =>
            {
                if (string.IsNullOrEmpty(src.Name)) { throw new ApplicationException("Invalid preference name."); }
            });
    }
}