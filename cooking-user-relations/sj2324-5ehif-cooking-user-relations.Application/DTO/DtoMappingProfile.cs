using AutoMapper;
using sj2324_5ehif_cooking_user_relations.Application.Model;
using sj2324_5ehif_cooking_user_relations.Application.DTO;

public class DtoMappingProfile : Profile
{
    public DtoMappingProfile()
    {
        CreateMap<FeedbackDto, Feedback>()
           .ForMember(dst => dst.From, opt => opt.MapFrom(src => new User(src.FromUser.UserKey, src.FromUser.Name)))
           .ForMember(dst => dst.To, opt => opt.MapFrom(src => new User(src.ToUser.UserKey, src.ToUser.Name)))
           .ForMember(dst => dst.Recipe, opt => opt.MapFrom(src => new Recipe(src.Recipe.RecipeKey, src.Recipe.Name)))
           .ReverseMap();


        // Follow Mapping
        CreateMap<Follow, FollowDto>()
            .ForMember(dst => dst.FollowKey, opt => opt.MapFrom(src => src.Key.ToString()))
            .ForMember(dst => dst.Followed, opt => opt.MapFrom(src => src.User))
            .ForMember(dst => dst.Follower, opt => opt.MapFrom(src => src.Follower))
            .ReverseMap();

        // RecipeShare Mapping
        CreateMap<RecipeShare, RecipeShareDto>()
            .ForMember(dst => dst.RecipeShareKey, opt => opt.MapFrom(src => src.Key.ToString()))
            .ForMember(dst => dst.Collaborators, opt => opt.MapFrom(src => src.Collaborators))
            .ForMember(dst => dst.Recipe, opt => opt.MapFrom(src => src.Recipe))
            .ReverseMap();

        // User Mapping
        CreateMap<User, UserDto>()
            .ForMember(dst => dst.UserKey, opt => opt.MapFrom(src => src.Id))
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();

        // User Mapping
        CreateMap<Recipe, RecipeDto>()
            .ForMember(dst => dst.RecipeKey, opt => opt.MapFrom(src => src.Id))
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();
    }
}
