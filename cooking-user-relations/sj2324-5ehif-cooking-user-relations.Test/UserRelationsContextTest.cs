using Microsoft.EntityFrameworkCore;
using sj2324_5ehif_cooking_user_relations.Application.Infrastructure;
using sj2324_5ehif_cooking_user_relations.Application.Model;
namespace sj2324_5ehif_cooking_user_relations.Test;
public class UserRelationsContextTests
{
    private UserRelationsContext _context;

    public UserRelationsContextTests()
    {
        var options = new DbContextOptionsBuilder<UserRelationsContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new UserRelationsContext(options);
    }

    [Fact]
    public void RecipeShareTest()
    {
        var testRecipe = new Recipe("Test Recipe");
        _context.Recipes.Add(testRecipe);

        var testUser1 = new User("USR123", "John Doe");
        var testUser2 = new User("USR456", "Jane Doe");

        var collaborators = new List<User> { testUser1, testUser2 };

        var recipeShare = new RecipeShare(testRecipe, collaborators);

        _context.RecipeShares.Add(recipeShare);
        _context.SaveChanges();

        var retrievedRecipeShare = _context.RecipeShares
            .Include(rs => rs.Collaborators) 
            .FirstOrDefault(rs => rs.Key == recipeShare.Key);

        Assert.NotNull(retrievedRecipeShare);
        Assert.Equal(testRecipe.Id, retrievedRecipeShare.Recipe.Id); 
        Assert.Equal(collaborators.Count, retrievedRecipeShare.Collaborators.Count);
    }
    [Fact]
    public void AddRetrieveFeedbackTest()
    {
        var testUser1 = new User("USR123", "John Doe");
        var testUser2 = new User("USR456", "Jane Doe");
        var testRecipe = new Recipe("Test Recipe");

        var feedback = new Feedback(testUser1, testUser2, 5, testRecipe, "Great recipe!");

        _context.Feedbacks.Add(feedback);
        _context.SaveChanges();

        var retrievedFeedback = _context.Feedbacks.FirstOrDefault(f => f.Id == feedback.Id);
        Assert.NotNull(retrievedFeedback);
        Assert.Equal("Great recipe!", retrievedFeedback.Text);
    }


    [Fact]
    public void AddRetrieveFollowTest()
    {
        var follower = new User("USR789", "Alice");
        var followed = new User("USR101", "Bob");


        _context.Users.AddRange(followed, follower);
        _context.SaveChanges();

        var follow = new Follow(followed, follower);
        _context.Follows.Add(follow);
        _context.SaveChanges();
        var retrievedFollow = _context.Follows.FirstOrDefault(f => f.Id == follow.Id);

        Assert.NotNull(retrievedFollow);
        Assert.Equal(followed.Id, retrievedFollow.User.Id);
        Assert.Equal(follower.Id, retrievedFollow.Follower.Id);
    }

    [Fact]
    public void AddUpdateRecipeTest()
    {
        var recipe = new Recipe("Original Recipe Name");
        _context.Recipes.Add(recipe);
        _context.SaveChanges();

        var recipeToUpdate = _context.Recipes.FirstOrDefault(r => r.Id == recipe.Id);
        recipeToUpdate.Name = "Updated Recipe Name";
        _context.SaveChanges();

        var updatedRecipe = _context.Recipes.FirstOrDefault(r => r.Id == recipe.Id);
        Assert.NotNull(updatedRecipe);
        Assert.Equal("Updated Recipe Name", updatedRecipe.Name);
    }

    [Fact]
    public void DeleteRecipeShareTest()
    {
        var testRecipe = new Recipe("Test Recipe for Deletion");
        var testUser = new User("USR7889", "Charlie");
        var collaborators = new List<User> { testUser };

        var recipeShare = new RecipeShare(testRecipe, collaborators);

        _context.RecipeShares.Add(recipeShare);
        _context.SaveChanges();

        _context.RecipeShares.Remove(recipeShare);
        _context.SaveChanges();

        var retrievedRecipeShare = _context.RecipeShares.FirstOrDefault(rs => rs.Id == recipeShare.Id);
        Assert.Null(retrievedRecipeShare);
    }
    [Fact]
    public void AddRetrieveUserTest()
    {
        var newUser = new User("USR888", "Alma");

        _context.Users.Add(newUser);
        _context.SaveChanges();

        var retrievedUser = _context.Users.FirstOrDefault(u => u.Id == newUser.Id);
        Assert.NotNull(retrievedUser);
        Assert.Equal("Alma", retrievedUser.Name);
    }
    [Fact]
    public void UpdateUserTest()
    {
        var user = new User("USR999", "Eve");
        _context.Users.Add(user);
        _context.SaveChanges();

        var userToUpdate = _context.Users.FirstOrDefault(u => u.Id == user.Id);
        userToUpdate.Name = "Alma";
        _context.SaveChanges();

        var updatedUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
        Assert.NotNull(updatedUser);
        Assert.Equal("Alma", updatedUser.Name);
    }

}