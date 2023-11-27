using AutoMapper;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using sj2324_5ehif_cooking_user.Application.DTO;
using sj2324_5ehif_cooking_user.Application.Infrastructure;
using sj2324_5ehif_cooking_user.Webapi.Controllers;
using sj2324_5ehif_cooking_user.Webapi.Services;

namespace sj2324_5ehif_cooking_user.Test.UserTests;

public class UserTest
{
    private readonly ILogger<AuthController> _mockAuthLogger;
    private readonly IConfiguration _mockConfiguration;
    private readonly UserContext _mockContext;
    private readonly JwtUtils _mockJwtUtils;
    private readonly ILogger<UserController> _mockLogger;
    private readonly IMapper _mockMapper;
    private readonly IPasswordUtils _mockPasswordUtils;

    public UserTest()
    {
        var inMemorySettings = new Dictionary<string, string>
        {
            {
                "Jwt:Key",
                "ecawiasqrpqrgyhwnolrudpbsrwaynbqdayndnmcehjnwqyouikpodzaqxivwkconwqbhrmxfgccbxbyljguwlxhdlcvxlutbnwjlgpfhjgqbegtbxbvwnacyqnltrby"
            },
            { "Jwt:Issuer", "CookingUser" },
            { "Jwt:Audience", "CookingUser" }
        };


        _mockConfiguration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var options = new DbContextOptionsBuilder<UserContext>()
            .UseInMemoryDatabase("TestDatabase")
            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            .Options;

        _mockContext = new UserContext(options);

        _mockLogger = new Logger<UserController>(new LoggerFactory());
        _mockAuthLogger = new Logger<AuthController>(new LoggerFactory());
        _mockJwtUtils = new JwtUtils(_mockConfiguration);
        _mockPasswordUtils = new PasswordUtils();
        var faker = new Faker();
        var registerModel = new RegisterModel
        {
            Username = faker.Internet.UserName(),
            Lastname = faker.Name.LastName(),
            Firstname = faker.Name.FirstName(),
            Email = faker.Internet.Email(),
            Password = "validpassword"
        };
        RegistrationHelper(registerModel);
    }

    [Fact]
    public async Task DeleteUser_InvalidCredentials_ReturnsUnauthorized()
    {
        var controller = new UserController(_mockContext, _mockLogger, _mockPasswordUtils, _mockMapper);

        var userToDelete = _mockContext.Users.First();

        var deleteModel = new DeleteUserModel
        {
            Username = userToDelete.Username,
            Email = userToDelete.Email,
            Password = "invalidpassword"
        };

        var result = await controller.DeleteUser(deleteModel) as UnauthorizedObjectResult;

        Assert.NotNull(result);
        Assert.Equal("Invalid credentials", result.Value);
    }

    [Fact]
    public async Task DeleteUser_ReturnsOk()
    {
        var controller = new UserController(_mockContext, _mockLogger, _mockPasswordUtils, _mockMapper);

        var userToDelete = _mockContext.Users.First();

        var deleteModel = new DeleteUserModel
        {
            Username = userToDelete.Username,
            Email = userToDelete.Email,
            Password = "validpassword"
        };

        var result = await controller.DeleteUser(deleteModel) as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal("User deleted successfully", result.Value);
    }

    //Helper methods
    private async void RegistrationHelper(RegisterModel registerModel)
    {
        var controller = new AuthController(_mockContext, _mockAuthLogger,
            _mockJwtUtils, _mockPasswordUtils);

        await controller.Register(registerModel);
    }
}