using AutoMapper;
using sj2324_5ehif_cooking_user.Application.DTO;
using sj2324_5ehif_cooking_user.Application.Model;
namespace sj2324_5ehif_cooking_user.Application.DTO
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>()
                .BeforeMap((src, dst) =>
                {
                    if (string.IsNullOrEmpty(src.Username)) { throw new ApplicationException("Invalid username"); }
                    if (string.IsNullOrEmpty(src.Lastname)) { throw new ApplicationException("Invalid lastname"); }
                    if (string.IsNullOrEmpty(src.Firstname)) { throw new ApplicationException("Invalid firstname"); }
                    if (string.IsNullOrEmpty(src.Email)) { throw new ApplicationException("Invalid email"); }
                });

            CreateMap<Cookbook, CookbookDto>();
            CreateMap<CookbookDto, Cookbook>()
                .BeforeMap((src, dst) =>
                {
                    if (string.IsNullOrEmpty(src.Name)) { throw new ApplicationException("Invalid cookbook name."); }
                });

            CreateMap<RecipeDto, Recipe>().ConstructUsing(dto => new Recipe(
                new RecipeKey(dto.RecipeKey ?? string.Empty),
                dto.Name ?? string.Empty
            )).BeforeMap((src, dst) =>
            {
                if (string.IsNullOrEmpty(src.Name)) { throw new ApplicationException("Invalid recipe name."); }
            });

            CreateMap<Preference, PreferenceDto>(); CreateMap<PreferenceDto, Preference>()
    .BeforeMap((src, dst) =>
    {
        if (string.IsNullOrEmpty(src.Name)) { throw new ApplicationException("Invalid preference key."); }
        if (string.IsNullOrEmpty(src.PreferenceKey)) { throw new ApplicationException("Invalid preference name."); }
    })
    .ConstructUsing(dto => new Preference(new PreferenceKey(dto.PreferenceKey), dto.Name));


        }
    }
}