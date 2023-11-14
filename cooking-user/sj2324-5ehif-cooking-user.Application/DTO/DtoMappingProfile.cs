using AutoMapper;
using sj2324_5ehif_cooking_user.Application.Model;

namespace sj2324_5ehif_cooking_user.Application.DTO
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<UserDto, User>()
                .BeforeMap((src, dst) =>
                {
                    if (string.IsNullOrEmpty(src.Username)) { throw new ApplicationException("Invalid username"); }
                    if (string.IsNullOrEmpty(src.Lastname)) { throw new ApplicationException("Invalid lastname"); }
                    if (string.IsNullOrEmpty(src.Firstname)) { throw new ApplicationException("Invalid firstname"); }
                    if (string.IsNullOrEmpty(src.Email)) { throw new ApplicationException("Invalid email"); }
                });
            
            CreateMap<PreferenceDto, Preference>()
                .ConstructUsing(dto => new Preference(dto.Name));
            
            CreateMap<User, UserDto>()
             .BeforeMap((src, dst) => dst.UserKey = src.Key.Value);

            CreateMap<Preference, PreferenceDto>()
                .BeforeMap((src, dst) => dst.PreferenceKey = src.ProxyKey.Value);
            
            CreateMap<Cookbook, CookbookDto>()
              .ForMember(dest => dest.CookbookKey, opt => opt.MapFrom(src => src.Key.Value));

            CreateMap<Recipe, RecipeDto>()
                .ForMember(dest => dest.RecipeKey, opt => opt.MapFrom(src => src.ProxyId.Value));

            CreateMap<CookbookDto, Cookbook>()
                .BeforeMap((src, dst) =>
                {
                    if (string.IsNullOrEmpty(src.Name)) { throw new ApplicationException("Invalid cookbook name."); }
                });

            
                     
            CreateMap<PreferenceDto, Preference>()
                .ConstructUsing(dto => new Preference(dto.Name));

            CreateMap<RecipeDto, Recipe>()
            .ConstructUsing(dto => new Recipe(dto.Name)).
            BeforeMap((src, dst) =>
            {
                if (string.IsNullOrEmpty(src.Name)) { throw new ApplicationException("Invalid recipe name."); }
            });

        }
    }
}