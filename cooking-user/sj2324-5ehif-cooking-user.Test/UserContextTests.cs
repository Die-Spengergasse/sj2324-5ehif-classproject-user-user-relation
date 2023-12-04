namespace sj2324_5ehif_cooking_user.Test;

using Microsoft.EntityFrameworkCore;
using Application.Infrastructure;
using Application.Model;

public class UserContextTests
{
    private readonly UserContext _context;

    public UserContextTests()
    {
        var options = new DbContextOptionsBuilder<UserContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        _context = new UserContext(options);
    }

    [Fact]
    public void UpdateCookbookTest()
    {
        var testCookbook = new Cookbook("USR1234567890AB", "Test Cookbook2", false);

        _context.Cookbooks.Add(testCookbook);
        _context.SaveChanges();

        var cookbookToUpdate = _context.Cookbooks.FirstOrDefault(c => c.Name == "Test Cookbook2");
        if (cookbookToUpdate != null)
        {
            cookbookToUpdate.Name = "Updated Cookbook Name";
            _context.SaveChanges();
        }

        Assert.True(_context.Cookbooks.Any(c => c.Name == "Updated Cookbook Name"));
    }

    [Fact]
    public void AddCookbookTest()
    {
        var testCookbook = new Cookbook("USR1234567890AB", "Test AddCookbook", false);

        _context.Cookbooks.Add(testCookbook);
        _context.SaveChanges();

        Assert.True(_context.Cookbooks.Any(c => c.Name == "Test AddCookbook"));
    }

    [Fact]
    public void DeleteCookbookTest()
    {
        var testCookbook = new Cookbook("USR1234567890AB", "Test AddCookbook3", false);

        _context.Cookbooks.Add(testCookbook);
        _context.SaveChanges();

        var cookbookToDelete = _context.Cookbooks.FirstOrDefault(c => c.Name == "Test Cookbook3");
        if (cookbookToDelete != null)
        {
            _context.Cookbooks.Remove(cookbookToDelete);
            _context.SaveChanges();
        }

        Assert.False(_context.Cookbooks.Any(c => c.Name == "Test Cookbook3"));
    }

    [Fact]
    public void AddRecipeToCookbookTest()
    {
        var testCookbook = new Cookbook("USR1234567890AB", "Test Cookbook", false);

        _context.Cookbooks.Add(testCookbook);
        _context.SaveChanges();

        var testRecipe = new Recipe("Der Augenschmaus", "USR1234567890AB");
        var cookbook = _context.Cookbooks.FirstOrDefault(c => c.Name == "Test Cookbook");
        cookbook.AddRecipe(testRecipe);
        _context.SaveChanges();

        Assert.Contains(testRecipe, cookbook.Recipes);
    }

    [Fact]
    public void AddUserToCollaboratorsTest()
    {
        var testCookbook = new Cookbook("USR1234567890AB", "Test Cookbook", false);

        _context.Cookbooks.Add(testCookbook);
        _context.SaveChanges();

        var newUser = new User("JUNGES HAUS", "Kalckstin", "Luis2", "luisk1@outlook.at", "test123");

        var cookbook = _context.Cookbooks.FirstOrDefault(c => c.Name == "Test Cookbook");
        cookbook.AddUser(newUser);
        _context.SaveChanges();

        Assert.Contains(newUser, cookbook.Collaborators);
    }


    [Fact]
    public void AddRecipeTest()
    {
        var testRecipe = new Recipe("Test Recipe", "USR1234567890AB");
        var testRecipyKey = testRecipe.Key;
        _context.Recipes.Add(testRecipe);
        _context.SaveChanges();

        Assert.Equal(1, _context.Recipes.Count());
        Assert.Contains(_context.Recipes, r => r.Key == testRecipyKey);
    }

    [Fact]
    public void DeleteRecipeTest()
    {
        var testRecipe = new Recipe("Test Recipe500", "USR1234567890AB");
        _context.Recipes.Add(testRecipe);
        _context.SaveChanges();

        if (testRecipe != null)
        {
            _context.Recipes.Remove(testRecipe);
            _context.SaveChanges();
        }

        Assert.False(_context.Recipes.Any(r => r.Key == "RECTESTINGIFITWORKS"));
    }
}