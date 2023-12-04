using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using sj2324_5ehif_cooking_user.Application.DTO;
using sj2324_5ehif_cooking_user.Application.Infrastructure;
using sj2324_5ehif_cooking_user.Application.Model;
using sj2324_5ehif_cooking_user.Application.Repository;
using sj2324_5ehif_cooking_user.Webapi.Controllers;
using sj2324_5ehif_cooking_user.Webapi.Services;

namespace sj2324_5ehif_cooking_user.Test.UserTests;

public class AuthTest
{
    private readonly IConfiguration _mockConfiguration;
    private readonly UserContext _mockContext;
    private readonly JwtUtils _mockJwtUtils;
    private readonly ILogger<AuthController> _mockLogger;

    public AuthTest()
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

        _mockLogger = new Logger<AuthController>(new LoggerFactory());
        _mockJwtUtils = new JwtUtils(_mockConfiguration);
    }

    [Fact]
    public async Task Register_ValidUser_ReturnsOk()
    {
        var faker = new Faker();
        var registerModel = new RegisterModel
        {
            Username = faker.Internet.UserName(),
            Lastname = faker.Name.LastName(),
            Firstname = faker.Name.FirstName(),
            Email = faker.Internet.Email(),
            Password = faker.Internet.Password()
        };

        var result = await RegistrationHelper(registerModel) as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal("Registration successful", result.Value);
    }

    [Fact]
    public async Task Login_ValidUser_ReturnsToken()
    {
        var faker = new Faker();
        var registerModel = new RegisterModel
        {
            Username = "testuser",
            Lastname = faker.Name.LastName(),
            Firstname = faker.Name.FirstName(),
            Email = faker.Internet.Email(),
            Password = "hashedpassword"
        };

        await RegistrationHelper(registerModel);


        var loginModel = new LoginModel
        {
            Username = "testuser",
            Password = "hashedpassword"
        };

        var result = await LoginHelper(loginModel) as OkObjectResult;

        Assert.NotNull(result.Value);
    }

    //Helper methods
    private async Task<IActionResult> RegistrationHelper(RegisterModel registerModel)
    {
        var controller = new AuthController(_mockLogger,
            _mockJwtUtils, new Repository<User>(_mockContext));

        return await controller.Register(registerModel);
    }

    private async Task<IActionResult> LoginHelper(LoginModel loginModel)
    {
        var controller = new AuthController(_mockLogger,
            _mockJwtUtils, new Repository<User>(_mockContext));

        return await controller.Login(loginModel);
    }
}