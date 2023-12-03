using AutoMapper;
using sj2324_5ehif_cooking_user_relations.Application.DTO;
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
    public void FeedbackDto_MapsCorrectly()
    {
        var feedbackDto = new FeedbackDto
        {
            FeedbackKey = "FBK123", 
            FromUser = new UserDto { UserKey = "USR123", Name = "JohnDoe" },
            ToUser = new UserDto { UserKey = "USR456", Name = "JaneDoe" },
            Rating = 5,
            Recipe = new RecipeDto { RecipeKey = "REC1234567890AB", Name = "Cheese Cake" },
            Text = "Great recipe!"
        };

        var feedback = _mapper.Map<Feedback>(feedbackDto);

        Assert.NotNull(feedback);
        Assert.Equal(feedbackDto.FeedbackKey, feedback.Key.ToString());
        Assert.Equal(feedbackDto.FromUser.UserKey, feedback.From.Key.ToString());
        Assert.Equal(feedbackDto.ToUser.UserKey, feedback.To.Key.ToString());
        Assert.Equal(feedbackDto.Rating, feedback.Rating);
        Assert.Equal(feedbackDto.Recipe.RecipeKey, feedback.Recipe.Key.ToString());
        Assert.Equal(feedbackDto.Text, feedback.Text);
    }


    [Fact]
    public void AutoMapper_RecipeDto_MapsCorrectly()
    {
        var recipeDto = new RecipeDto
        {
            RecipeKey = "RCP321",
            Name = "Apple Pie"
        };

        var recipe = _mapper.Map<Recipe>(recipeDto);

        Assert.NotNull(recipe);
        Assert.Equal(recipeDto.RecipeKey, recipe.Key.ToString());
        Assert.Equal(recipeDto.Name, recipe.Name);
    }
    [Fact]
    public void AutoMapper_UserDto_MapsCorrectly()
    {
        var userDto = new UserDto
        {
            UserKey = "USR789",
            Name = "Alice"
        };

        var user = _mapper.Map<User>(userDto);

        Assert.NotNull(user);
        Assert.Equal(userDto.UserKey, user.Key.ToString());
        Assert.Equal(userDto.Name, user.Name);
    }

    [Fact]
    public void Feedback_ToFeedbackDto_AndBack_MapsCorrectly()
    {
        var feedback = new Feedback(
            new User("USR123", "JohnDoe"),
            new User("USR456", "JaneDoe"),
            5,
            new Recipe("REC1234567890AB", "Cheese Cake"),
            "Great recipe!"
        );

        var feedbackDto = _mapper.Map<FeedbackDto>(feedback);
        var feedbackMappedBack = _mapper.Map<Feedback>(feedbackDto);

        Assert.Equal(feedback.Text, feedbackMappedBack.Text);
        Assert.Equal(feedback.Rating, feedbackMappedBack.Rating);
        Assert.Equal(feedback.From.Key, feedbackMappedBack.From.Key);
        Assert.Equal(feedback.To.Key, feedbackMappedBack.To.Key);
        Assert.Equal(feedback.Recipe.Key, feedbackMappedBack.Recipe.Key);
    }

    [Fact]
    public void AutoMapper_ApplicationException_ThrownForInvalidRecipeDto()
    {
        var recipeDto = new RecipeDto
        {
            RecipeKey = "REC1234567890AB",
        };

        Assert.Throws<ApplicationException>(() => _mapper.Map<Recipe>(recipeDto));
    }
    [Fact]
    public void AutoMapper_ApplicationException_ThrownForInvalidUserDto()
    {
        var userDto = new UserDto
        {
            UserKey = "USR1234567890AB",
        };

        Assert.Throws<ApplicationException>(() => _mapper.Map<User>(userDto));
    }

    [Fact]
    public void FollowDtoToFollow_MapsCorrectly()
    {
        var followDto = new FollowDto
        {
            FollowKey = "FLK123",
            Follower = new UserDto { UserKey = "USR123", Name = "JohnDoe" },
            User = new UserDto { UserKey = "USR456", Name = "JaneDoe" }
        };

        var follow = _mapper.Map<Follow>(followDto);

        Assert.NotNull(follow);
        Assert.Equal(followDto.FollowKey, follow.Key);
        Assert.Equal(followDto.Follower.UserKey, follow.Follower.Key);
        Assert.Equal(followDto.Follower.Name, follow.Follower.Name);
        Assert.Equal(followDto.User.UserKey, follow.User.Key);
        Assert.Equal(followDto.User.Name, follow.User.Name);
    }


    [Fact]
    public void FollowToFollowDto_MapsCorrectly()
    {
        var follow = new Follow(new User("USR123", "JohnDoe"), new User("USR456", "JaneDoe"));

        var followDto = _mapper.Map<FollowDto>(follow);

        Assert.NotNull(followDto);
        Assert.Equal(follow.Key, followDto.FollowKey);
        Assert.Equal(follow.Follower.Key, followDto.Follower.UserKey);
        Assert.Equal(follow.Follower.Name, followDto.Follower.Name);
        Assert.Equal(follow.User.Key, followDto.User.UserKey);
        Assert.Equal(follow.User.Name, followDto.User.Name);
    }


    [Fact]
    public void RecipeShareDtoToRecipeShare_MapsCorrectly()
    {
        var recipeShareDto = new RecipeShareDto
        {
            RecipeShareKey = "RSK123",
            Recipe = new RecipeDto { RecipeKey = "REC123", Name = "Apple Pie" },
            Collaborators = new List<UserDto>
        {
            new UserDto { UserKey = "USR123", Name = "JohnDoe" },
            new UserDto { UserKey = "USR456", Name = "JaneDoe" }
        }
        };

        var recipeShare = _mapper.Map<RecipeShare>(recipeShareDto);

        Assert.NotNull(recipeShare);
        Assert.Equal(recipeShareDto.RecipeShareKey, recipeShare.Key);
        Assert.Equal(recipeShareDto.Recipe.RecipeKey, recipeShare.Recipe.Key);
        Assert.Equal(recipeShareDto.Recipe.Name, recipeShare.Recipe.Name);
        Assert.Equal(2, recipeShare.Collaborators.Count);
        Assert.Contains(recipeShare.Collaborators, c => c.Key == "USR123" && c.Name == "JohnDoe");
        Assert.Contains(recipeShare.Collaborators, c => c.Key == "USR456" && c.Name == "JaneDoe");
    }
    [Fact]
    public void RecipeShareToRecipeShareDto_MapsCorrectly()
    {
        var recipe = new Recipe("REC123", "Apple Pie");
        var collaborators = new List<User>
    {
        new User("USR123", "JohnDoe"),
        new User("USR456", "JaneDoe")
    };
        var recipeShare = new RecipeShare(recipe, collaborators);

        var recipeShareDto = _mapper.Map<RecipeShareDto>(recipeShare);

        Assert.NotNull(recipeShareDto);
        Assert.Equal(recipeShare.Key, recipeShareDto.RecipeShareKey);
        Assert.Equal(recipe.Key, recipeShareDto.Recipe.RecipeKey);
        Assert.Equal(recipe.Name, recipeShareDto.Recipe.Name);
        Assert.Equal(2, recipeShareDto.Collaborators.Count);
    }


}