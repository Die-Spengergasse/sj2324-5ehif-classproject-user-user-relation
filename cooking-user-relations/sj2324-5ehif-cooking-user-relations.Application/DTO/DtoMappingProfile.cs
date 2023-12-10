using AutoMapper;
using sj2324_5ehif_cooking_user_relations.Application.Model;
using sj2324_5ehif_cooking_user_relations.Application.DTO.Recipe;
using sj2324_5ehif_cooking_user_relations.Application.DTO.User;
using sj2324_5ehif_cooking_user_relations.Application.DTO.Follow;
using sj2324_5ehif_cooking_user_relations.Application.DTO.RecipeShare;
using sj2324_5ehif_cooking_user_relations.Application.DTO.Feedback;

public class DtoMappingProfile : Profile
{
    public DtoMappingProfile()
    {
        CreateMap<Feedback, FeedbackDto>();
        CreateMap<User, UserDto>();
        CreateMap<Recipe, RecipeDto>();
        CreateMap<Follow, FollowDto>();
        CreateMap<RecipeShare, RecipeShareDto>();
    }
}