using AutoMapper;
using sj2324_5ehif_cooking_user_relations.Application.DTO.Feedback;
using sj2324_5ehif_cooking_user_relations.Application.DTO.Follow;
using sj2324_5ehif_cooking_user_relations.Application.DTO.Recipe;
using sj2324_5ehif_cooking_user_relations.Application.DTO.RecipeShare;
using sj2324_5ehif_cooking_user_relations.Application.DTO.User;
using sj2324_5ehif_cooking_user_relations.Application.Model;

namespace sj2324_5ehif_cooking_user_relations.Test;

public class DtoTest
{
    private readonly IMapper _mapper;

    public DtoTest()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<DtoMappingProfile>());
        _mapper = config.CreateMapper();
    }

    [Fact]
    public void RecipeToRecipeDto()
    {
        var recipe = new Recipe("MyRecipe", "USR1234567890AB") { Key = "REC1234567890AB" };
        var recipeDto = _mapper.Map<RecipeDto>(recipe);

        Assert.NotNull(recipeDto);
        Assert.Equal(recipeDto.Name, recipe.Name);
        Assert.Equal(recipeDto.AuthorKey, recipe.AuthorKey);
        Assert.Equal(recipeDto.Key, recipe.Key);
    }
    [Fact]
    public void FollowToFollowDto()
    {
        // Arrange
        var user = new User("USR123", "JohnDoe");
        var follower = new User("USR456", "JaneDoe");
        var follow = new Follow(user, follower) { Key = "FLW123" };

        // Act
        var followDto = _mapper.Map<FollowDto>(follow);

        // Assert
        Assert.NotNull(followDto);

        Assert.Equal(follow.Follower.Key, followDto.Follower.Key);
        Assert.Equal(follow.Follower.Name, followDto.Follower.Name);
        Assert.Equal(follow.User.Key, followDto.User.Key);
        Assert.Equal(follow.User.Name, followDto.User.Name);
        Assert.Equal(follow.Key, followDto.Key);
    }

    [Fact]
    public void RecipeToRecipeShareDto()
    {
        var recipe = new Recipe("REC123", "Apple Pie");
        var collaborators = new List<User>
    {
        new User("USR123", "JohnDoe"),
        new User("USR456", "JaneDoe")
    };
        var recipeShare = new RecipeShare(recipe, collaborators) { Key = "RSK123" };

        var recipeShareDto = _mapper.Map<RecipeShareDto>(recipeShare);

        Assert.NotNull(recipeShareDto);
        Assert.Equal(recipeShare.Key, recipeShareDto.Key);
        Assert.Equal(recipe.Key, recipeShareDto.Recipe.Key);
        Assert.Equal(recipe.Name, recipeShareDto.Recipe.Name);
        Assert.Equal(2, recipeShareDto.Collaborators.Count);
        Assert.Contains(recipeShareDto.Collaborators, c => c.Key == "USR123" && c.Name == "JohnDoe");
        Assert.Contains(recipeShareDto.Collaborators, c => c.Key == "USR456" && c.Name == "JaneDoe");
    }


    [Fact]
    public void FeedbackToFeedbackDto()
    {
        var fromUser = new User("USR123", "JohnDoe");
        var toUser = new User("USR456", "JaneDoe");
        var recipe = new Recipe("REC789", "Cheese Cake") { Key = "REC789" };
        var feedback = new Feedback(fromUser, toUser, 5, recipe, "Great recipe!") { Key = "FBK012" };

        var feedbackDto = _mapper.Map<FeedbackDto>(feedback);

        Assert.NotNull(feedbackDto);
        Assert.Equal(feedback.Key, feedbackDto.Key);
        Assert.Equal(feedback.From.Key, feedbackDto.From.Key);
        Assert.Equal(feedback.To.Key, feedbackDto.To.Key);
        Assert.Equal(feedback.Recipe.Key, feedbackDto.Recipe.Key);
        Assert.Equal(feedback.Text, feedbackDto.Text);
    }

}