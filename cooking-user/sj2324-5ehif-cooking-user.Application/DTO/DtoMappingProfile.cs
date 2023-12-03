using System.ComponentModel.DataAnnotations;
using AutoMapper;
using sj2324_5ehif_cooking_user.Application.Model;
using ApplicationException = System.ApplicationException;

namespace sj2324_5ehif_cooking_user.Application.DTO
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<UserDto, User>()
                .BeforeMap((src, dst) =>
                {
                    // change to get values from DB
                    var list = new List<Preference>();
                    foreach (var preference in src.Preferences)
                    {
                        dst.AddPreference(new Preference(preference.Name){ Key = preference.PreferenceKey});
                        
                    }
                    dst.Password = src.Password;
                    dst.Key = src.UserKey;
                    if (string.IsNullOrEmpty(src.Username))
                    {
                        throw new ApplicationException("Invalid username");
                    }

                    if (string.IsNullOrEmpty(src.Lastname))
                    {
                        throw new ApplicationException("Invalid lastname");
                    }

                    if (string.IsNullOrEmpty(src.Firstname))
                    {
                        throw new ApplicationException("Invalid firstname");
                    }

                    if (string.IsNullOrEmpty(src.Email))
                    {
                        throw new ApplicationException("Invalid email");
                    }
                });
            
            CreateMap<PreferenceDto, Preference>()
                .ConstructUsing(dto => new Preference(dto.Name) { Key = dto.PreferenceKey });

            CreateMap<User, UserDto>()
                .BeforeMap((src, dst) => dst.UserKey = src.ObjectKey.Value)
                .ForSourceMember(source => source.Preferences, opt => opt.DoNotValidate());

            CreateMap<Preference, PreferenceDto>()
                .BeforeMap((src, dst) => dst.PreferenceKey = src.KeyObject.Value);

            CreateMap<Cookbook, CookbookDto>()
                .ForMember(dest => dest.CookbookKey, opt => opt.MapFrom(src => src.KeyObject.Value));

            CreateMap<Recipe, RecipeDto>()
                .ForMember(dest => dest.RecipeKey, opt => opt.MapFrom(src => src.KeyObject.Value));
        }
    }
}