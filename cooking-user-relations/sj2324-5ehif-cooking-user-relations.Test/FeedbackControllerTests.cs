using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using sj2324_5ehif_cooking_user_relations.Application.DTO.Feedback;
using Microsoft.AspNetCore.Mvc;
using Moq;
using sj2324_5ehif_cooking_user_relations.Application.DTO.Recipe;
using sj2324_5ehif_cooking_user_relations.Application.Model;
using sj2324_5ehif_cooking_user_relations.Application.Repository;
using sj2324_5ehif_cooking_user_relations.Webapi.Controllers;

public class FeedbackControllerTests
{
    private readonly Mock<IRepository<Feedback>> _mockFeedbackRepo;
    private readonly Mock<IRepository<User>> _mockUserRepo;
    private readonly Mock<IRepository<Recipe>> _mockRecipeRepo;
    private readonly Mock<HttpContext> _mockHttpContext;
    private readonly FeedbackController _controller;

    public FeedbackControllerTests()
    {
        _mockFeedbackRepo = new Mock<IRepository<Feedback>>();
        _mockUserRepo = new Mock<IRepository<User>>();
        _mockRecipeRepo = new Mock<IRepository<Recipe>>();
        _mockHttpContext = new Mock<HttpContext>();

        _controller = new FeedbackController(_mockFeedbackRepo.Object, _mockUserRepo.Object, _mockRecipeRepo.Object,
            _mockHttpContext.Object);
    }

    [Fact]
    public async Task AddFeedback_ReturnsCreated_WhenDataIsValid()
    {
        // Arrange
        var fakeUserKey = "testUserKey";
        var fakeUser = new User("testUserKey", "fakeName");
        var fakeToUser = new User("testToUserKey", "fakeToUser");
        var fakeRecipe = new Recipe("fakeRecipe", "fakeAuthor");
        var fakeRecipeDto = new RecipeDto
        {
            Key = "fakeRecipeKey",
            Name = "fakeRecipe",
            AuthorKey = "fakeAuthor"
        };
        var addFeedbackDto = new AddFeedbackDto
        {
            Rating = 3,
            Recipe = fakeRecipeDto,
            Text = "fake"
        };

        _mockHttpContext.SetupGet(x => x.User.Identity.Name).Returns(fakeUserKey);
        _mockUserRepo.Setup(x => x.GetByIdAsync(fakeUserKey)).ReturnsAsync((true, "got", fakeUser));
        _mockUserRepo.Setup(x => x.GetByIdAsync("fakeAuthor")).ReturnsAsync((true, "got", fakeToUser));
        _mockRecipeRepo.Setup(x => x.GetByIdAsync(addFeedbackDto.Recipe.Key)).ReturnsAsync((true, "got", fakeRecipe));
        _mockFeedbackRepo.Setup(x => x.InsertOneAsync(It.IsAny<Feedback>())).ReturnsAsync((true, "yes"));
        
        // Act
        var result = await _controller.AddFeedback(addFeedbackDto);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result);
        _mockFeedbackRepo.Verify(x => x.InsertOneAsync(It.IsAny<Feedback>()), Times.Once);
    }
    
    [Fact]
    public async Task AddFeedback_ReturnsUnauthorized_WhenUserNotLoggedIn()
    {
        // Arrange
        _mockHttpContext.SetupGet(x => x.User.Identity.Name).Returns((string)null);

        // Act
        var result = await _controller.AddFeedback(new AddFeedbackDto());

        // Assert
        Assert.IsType<UnauthorizedResult>(result);
    }
    
    [Fact]
    public async Task AddFeedback_ReturnsBadRequest_WhenLoggedInUserDoesNotExist()
    {
        // Arrange
        var fakeUserKey = "testUserKey";
        _mockHttpContext.SetupGet(x => x.User.Identity.Name).Returns(fakeUserKey);
        _mockUserRepo.Setup(x => x.GetByIdAsync(fakeUserKey)).ReturnsAsync((false, "not found", null));

        // Act
        var result = await _controller.AddFeedback(new AddFeedbackDto());

        // Assert
        var actionResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Logged in user doesnt exist", actionResult.Value);
    }
    
    [Fact]
    public async Task AddFeedback_ReturnsBadRequest_WhenRecipeDoesNotExist()
    {
        // Arrange
        var fakeUserKey = "testUserKey";
        var fakeUser = new User(fakeUserKey, "fakeName");
        _mockHttpContext.SetupGet(x => x.User.Identity.Name).Returns(fakeUserKey);
        _mockUserRepo.Setup(x => x.GetByIdAsync(fakeUserKey)).ReturnsAsync((true, "got", fakeUser));
        _mockRecipeRepo.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync((false, "not found", null));

        // Act
        var result = await _controller.AddFeedback(new AddFeedbackDto { Recipe = new RecipeDto { Key = "nonExistingRecipe" } });

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task GetFeedbackById_ReturnsFeedback()
    {
        var fakeUser = new User("testUserKey", "fakeName");
        var fakeToUser = new User("testToUserKey", "fakeToUser");
        var fakeRecipe = new Recipe("fakeRecipe", "fakeAuthor");

        var feedback = new Feedback(fakeUser, fakeToUser,3, fakeRecipe, "good recipe");

        _mockFeedbackRepo.Setup(x => x.GetByIdAsync("1")).ReturnsAsync((true, "null", feedback));
        
        var result = await _controller.GetFeedbackById("1");
        
        var actionResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equivalent(feedback, actionResult.Value);
    }
    [Fact]
    public async Task GetFeedbackByRecipeId_ReturnsOk_WhenDataExists()
    {
        // Arrange
        var fakeId = "testRecipeId";
        var fakeFeedbacks = new List<Feedback> { /* Initialize with test data */ };
        _mockFeedbackRepo.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Feedback, bool>>>()))
            .ReturnsAsync((true, "got", fakeFeedbacks));

        // Act
        var result = await _controller.GetFeedbackByRecipeId(fakeId);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equivalent(fakeFeedbacks, actionResult.Value);
    }
    
    [Fact]
    public async Task AddFeedback_ReturnsBadRequest_WhenFeedbackAlreadyExists()
    {
        // Arrange
        var fakeUserKey = "testUserKey";
        var fakeUser = new User(fakeUserKey, "fakeName");
        var fakeRecipeKey = "fakeRecipeKey";
        var fakeRecipe = new Recipe(fakeRecipeKey, fakeUserKey);
        var fakeToUser = new User("testToUserKey", "fakeToUser");
        var existingFeedback = new Feedback(fakeUser, fakeToUser, 3, fakeRecipe, "Existing feedback text");
        var existingFeedbackList = new List<Feedback>();
        existingFeedbackList.Add(existingFeedback);
        _mockHttpContext.SetupGet(x => x.User.Identity.Name).Returns(fakeUserKey);
        _mockUserRepo.Setup(x => x.GetByIdAsync(fakeUserKey)).ReturnsAsync((true, "got", fakeUser));
        _mockRecipeRepo.Setup(x => x.GetByIdAsync(fakeRecipeKey)).ReturnsAsync((true, "got", fakeRecipe));
        _mockUserRepo.Setup(x => x.GetByIdAsync(fakeRecipe.AuthorKey)).ReturnsAsync((true, "got", fakeToUser));
        _mockFeedbackRepo.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Feedback, bool>>>()))
            .ReturnsAsync((true, "found", existingFeedbackList));

        var addFeedbackDto = new AddFeedbackDto
        {
            Rating = 3,
            Recipe = new RecipeDto { Key = fakeRecipeKey },
            Text = "New feedback text"
        };

        // Act
        var result = await _controller.AddFeedback(addFeedbackDto);

        // Assert
        var actionResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Already Exists", actionResult.Value);
    }


    [Fact]
    public async Task GetFeedbackByRecipeId_ReturnsBadRequest_WhenDataDoesNotExist()
    {
        // Arrange
        _mockFeedbackRepo.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Feedback, bool>>>()))
            .ReturnsAsync((false, "not found", null));

        // Act
        var result = await _controller.GetFeedbackByRecipeId("nonExistingId");

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
    
    [Fact]
    public async Task GetAll_ReturnsOk_WhenDataExists()
    {
        // Arrange
        var fakeFeedbacks = new List<Feedback> { /* Initialize with test data */ };
        _mockFeedbackRepo.Setup(x => x.GetAllAsync()).ReturnsAsync((true, "got", fakeFeedbacks));

        // Act
        var result = await _controller.GetAll();

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equivalent(fakeFeedbacks, actionResult.Value);
    }

    [Fact]
    public async Task GetAll_ReturnsBadRequest_WhenDataDoesNotExist()
    {
        // Arrange
        _mockFeedbackRepo.Setup(x => x.GetAllAsync()).ReturnsAsync((false, "not found", null));

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task UpdateFeedback_ReturnsOk_WhenUpdateIsSuccessful()
    {
        // Arrange
        var fakeUser = new User("testUserKey", "fakeName");
        var fakeToUser = new User("testToUserKey", "fakeToUser");
        var fakeRecipe = new Recipe("fakeRecipe", "fakeAuthor");
        
        var fakeId = "testFeedbackId";
        var fakeFeedback = new Feedback(fakeUser,fakeToUser,3,fakeRecipe,"bad recipe");
        var fakeUpdateFeedbackDto = new UpdateFeedbackDto
        {
            Rating = 5,
            Text = "good recipe!!"
        };

        _mockFeedbackRepo.Setup(x => x.GetByIdAsync(fakeId)).ReturnsAsync((true, "got", fakeFeedback));
        _mockFeedbackRepo.Setup(x => x.UpdateOneAsync(It.IsAny<Feedback>())).ReturnsAsync((true, "updated"));

        // Act
        var result = await _controller.UpdateFeedback(fakeUpdateFeedbackDto, fakeId);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task UpdateFeedback_ReturnsBadRequest_WhenFeedbackNotFound()
    {
        // Arrange
        _mockFeedbackRepo.Setup(x => x.GetByIdAsync("nonExistingId")).ReturnsAsync((false, "not found", null));

        // Act
        var result = await _controller.UpdateFeedback(new UpdateFeedbackDto(), "nonExistingId");

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
    [Fact]
    public async Task DeleteFeedback_ReturnsOk_WhenDeleteIsSuccessful()
    {
        // Arrange
        _mockFeedbackRepo.Setup(x => x.DeleteOneAsync("testFeedbackId")).ReturnsAsync((true, "deleted"));

        // Act
        var result = await _controller.DeleteFeedback("testFeedbackId");

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteFeedback_ReturnsBadRequest_WhenFeedbackNotFound()
    {
        _mockFeedbackRepo.Setup(x => x.DeleteOneAsync("nonExistingId")).ReturnsAsync((false, "not found"));
        var result = await _controller.DeleteFeedback("nonExistingId");

        Assert.IsType<BadRequestResult>(result);
    }
}