using sj2324_5ehif_cooking_user.Application.Model;
using sj2324_5ehif_cooking_user.Application.Repository;
using Bogus;
using Assert = NUnit.Framework.Assert;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace sj2324_5ehif_cooking_user.Test;

public class RepositoryTests : DatabaseTest
{
    private readonly IRepository<User> _userRepository;

    public RepositoryTests()
    {
        _userRepository = new Repository<User>(_context);
    }

    public static string CreatePassword(int length)
    {
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        var res = new StringBuilder();
        var rnd = new Random();
        while (0 < length--) res.Append(valid[rnd.Next(valid.Length)]);
        return res.ToString();
    }

    public static User GenerateUser()
    {
        var faker = new Faker<User>()
            .CustomInstantiator(f => new User(
                    f.Person.UserName,
                    firstname: f.Person.FirstName,
                    lastname: f.Person.LastName,
                    email: f.Person.Email,
                    password: CreatePassword(8)
                )
            );
        return faker.Generate();
    }

    public async void SetupContext()
    {
        _context.Set<User>().RemoveRange(await _context.Set<User>().ToListAsync());
        await _context.SaveChangesAsync();
    }

    public async Task<User> AddUser()
    {
        var user = GenerateUser();
        await _context.Set<User>().AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }


    [Fact]
    public async Task AddUserTest()
    {
        // Prep
        SetupContext();
        var user1 = GenerateUser();

        // Act
        var result = await _userRepository.InsertOneAsync(user1);

        // Check
        var check = await _context.Set<User>().SingleOrDefaultAsync(e => e.Key == user1.Key);

        // Assert
        Assert.IsTrue(result.success);
        Assert.IsNotNull(check);
        Assert.AreEqual(check?.Key, user1.Key);

        // Clean
        SetupContext();
    }

    [Fact]
    public async Task GetByKeyTest()
    {
        // Prep
        SetupContext();
        var user = AddUser().Result;

        // Act
        var result = await _userRepository.GetByKeyAsync(user.Key);

        // Assert
        Assert.AreEqual(user.Key, result.entity.Key);
        Assert.IsTrue(result.success);
        Assert.IsNotNull(result.entity);
    }

    [Fact]
    public async Task GetAllUsersTest()
    {
        // Prep
        SetupContext();
        var user1 = AddUser();
        var user2 = AddUser();

        // Act
        var result = await _userRepository.GetAllAsync();

        // Assert
        Assert.IsTrue(result.success);
        Assert.IsNotNull(result.entity);
        Assert.AreEqual(2, result.entity.Count);

        // Clean
        SetupContext();
    }

    [Fact]
    public async Task UpdateUserTest()
    {
        // Prep
        SetupContext();
        var user = AddUser().Result;

        user.Email = "LuisStinkt@gmail.com";
        // Act
        var result = await _userRepository.UpdateOneAsync(user);

        // Check
        var updated_user = await _context.Set<User>().SingleOrDefaultAsync(e => e.Key == user.Key);

        // Assert
        Assert.IsTrue(result.success);
        Assert.AreEqual(user.Email, updated_user?.Email);

        // Clean
        SetupContext();
    }

    [Fact]
    public async Task DeleteUserTest()
    {
        // Prep
        SetupContext();
        var user = AddUser().Result;
        var check_user = await _context.Set<User>().SingleOrDefaultAsync(e => e.Key == user.Key);

        // Check
        if (check_user is not null)
        {
            // Act
            var result = await _userRepository.DeleteOneAsync(check_user.Key);

            var check = await _context.Set<User>().SingleOrDefaultAsync(e => e.Key == user.Key);

            // Assert
            Assert.IsTrue(result.success, "Deletion failed");
            Assert.IsNull(check, "User still exists after deletion");
            Assert.AreEqual(0, _context.Set<User>().Count(), "User was not deleted");
        }
        else
        {
            Assert.Fail("User not found after insertion");
        }
    }
}