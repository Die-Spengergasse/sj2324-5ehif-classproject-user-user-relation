using Microsoft.EntityFrameworkCore;
using sj2324_5ehif_cooking_user_relations.Application.Infrastructure;
using sj2324_5ehif_cooking_user_relations.Application.Model;

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
    public void TestRecipeShareRelationship()
    {
        // Create test Recipe
        var testRecipe = new Recipe("Test Recipe") { Id = "RECTESTINGIFITWORKS" };

        // Create test Users for collaborators
       
        var testUser1 = new User("USR123", "Kalckstein");
        var testUser2 = new User("USR124", "Kalckste1in");

        var collaborators = new List<User> { testUser1, testUser2 };

        // Create RecipeShare
        var recipeShare = new RecipeShare(testRecipe, collaborators);

        // Add RecipeShare to context and save
        _context.RecipeShares.Add(recipeShare);
        _context.SaveChanges();

        // Retrieve the added RecipeShare
        var retrievedRecipeShare = _context.RecipeShares
            .Include(rs => rs.Recipe)
            .Include(rs => rs.Collaborators)
            .FirstOrDefault(rs => rs.Key == recipeShare.Key);

        // Assertions
        Assert.NotNull(retrievedRecipeShare);
        Assert.Equal(testRecipe, retrievedRecipeShare.Recipe);
        Assert.Equal(2, retrievedRecipeShare.Collaborators.Count);
        Assert.Contains(testUser1, retrievedRecipeShare.Collaborators);
        Assert.Contains(testUser2, retrievedRecipeShare.Collaborators);
    }


}