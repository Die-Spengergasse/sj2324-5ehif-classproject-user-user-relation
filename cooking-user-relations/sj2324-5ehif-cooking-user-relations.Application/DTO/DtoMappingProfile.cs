
using AutoMapper;
using sj2324_5ehif_cooking_user_relations.Application.Model;
using sj2324_5ehif_cooking_user_relations.Application.DTO;

public class DtoMappingProfile : Profile
{
    public DtoMappingProfile()
    {
        CreateMap<FeedbackDto, Feedback>()
            .ConstructUsing(src => new Feedback(
                new User(src.FromUser.UserKey, src.FromUser.Name),
                new User(src.ToUser.UserKey, src.ToUser.Name),
                src.Rating,
                new Recipe(src.Recipe.RecipeKey, src.Recipe.Name),
                src.Text))
            .ForMember(dst => dst.Key, opt => opt.MapFrom(src => src.FeedbackKey));

        CreateMap<Feedback, FeedbackDto>()
          .ForMember(dst => dst.FeedbackKey, opt => opt.MapFrom(src => src.Key.ToString()))
          .ForMember(dst => dst.FromUser, opt => opt.MapFrom(src => new UserDto { UserKey = src.From.Key, Name = src.From.Name }))
          .ForMember(dst => dst.ToUser, opt => opt.MapFrom(src => new UserDto { UserKey = src.To.Key, Name = src.To.Name }))
          .ForMember(dst => dst.Recipe, opt => opt.MapFrom(src => new RecipeDto { RecipeKey = src.Recipe.Key, Name = src.Recipe.Name }));


        // Follow Mapping
        CreateMap<FollowDto, Follow>()
            .ConstructUsing(src =>
                new Follow(
                    new User(src.User.UserKey, src.User.Name),
                    new User(src.Follower.UserKey, src.Follower.Name)
                ))
            .ForMember(dst => dst.Key, opt => opt.MapFrom(src => src.FollowKey))
            .ReverseMap();

        CreateMap<Follow, FollowDto>()
        .ForMember(dst => dst.FollowKey, opt => opt.MapFrom(src => src.Key))
        .ForMember(dst => dst.Follower, opt => opt.MapFrom(src => new UserDto { UserKey = src.Follower.Key, Name = src.Follower.Name }))
        .ForMember(dst => dst.User, opt => opt.MapFrom(src => new UserDto { UserKey = src.User.Key, Name = src.User.Name }));


        // RecipeShare Mapping
        CreateMap<RecipeShare, RecipeShareDto>()
            .ForMember(dst => dst.RecipeShareKey, opt => opt.MapFrom(src => src.Key.ToString()))
            .ForMember(dst => dst.Collaborators, opt => opt.MapFrom(src => src.Collaborators))
            .ForMember(dst => dst.Recipe, opt => opt.MapFrom(src => src.Recipe));


        CreateMap<RecipeShareDto, RecipeShare>()
       .ForMember(dst => dst.Key, opt => opt.MapFrom(src => src.RecipeShareKey))
        .ForMember(dst => dst.Recipe, opt => opt.MapFrom(src => new Recipe(src.Recipe.RecipeKey, src.Recipe.Name)))
        .ForMember(dst => dst.Collaborators, opt => opt.MapFrom(src =>
        src.Collaborators.Select(dto => new User(dto.UserKey, dto.Name)).ToList()));

        // User Mapping
        CreateMap<User, UserDto>()
            .ForMember(dst => dst.UserKey, opt => opt.MapFrom(src => src.Key))
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<UserDto, User>()
         .BeforeMap((src, dst) =>
         {
       
         dst.Key = src.UserKey;
         if (string.IsNullOrEmpty(src.Name))
         {
            throw new ApplicationException("Invalid username");
         } 

        });

    
        CreateMap<Recipe, RecipeDto>()
            .ForMember(dst => dst.RecipeKey, opt => opt.MapFrom(src => src.Key))
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();

        CreateMap<RecipeDto, Recipe>()
           .ConstructUsing(dto => new Recipe(dto.Name) { Key = dto.RecipeKey }).BeforeMap((src, dst) =>
           {
               if (string.IsNullOrEmpty(src.Name))
               {
                   throw new ApplicationException("Invalid recipe name.");
               }
           });
    }
}