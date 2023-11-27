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
    public void AutoMapper_FeedbackDto_MapsCorrectly()
    {
        var feedbackDto = new FeedbackDto
        {
            FeedbackKey = "FBK123", // Example key
            FromUser = new UserDto { UserKey = "USR123", Name = "JohnDoe" },
            ToUser = new UserDto { UserKey = "USR456", Name = "JaneDoe" },
            Rating = 5,
            Recipe = new RecipeDto { RecipeKey = "REC1234567890AB", Name = "Cheese Cake" },
            Text = "Great recipe!"
        };

        // Ensure that your AutoMapper configuration is set up to handle these mappings correctly
        var feedback = _mapper.Map<Feedback>(feedbackDto);


        Assert.NotNull(feedback);
        Assert.Equal(feedbackDto.FeedbackKey, feedback.Key.ToString());
        //Assert.Equal(feedbackDto.FromUser.UserKey, feedback.From.Key.ToString());
        //Assert.Equal(feedbackDto.ToUser.UserKey, feedback.To.Key.ToString());
        Assert.Equal(feedbackDto.Rating, feedback.Rating);
        //Assert.Equal(feedbackDto.Recipe.RecipeKey, feedback.Recipe.Key.ToString());
        Assert.Equal(feedbackDto.Text, feedback.Text);
    }


}