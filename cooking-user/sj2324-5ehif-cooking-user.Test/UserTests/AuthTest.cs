using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using sj2324_5ehif_cooking_user.Application.Infrastructure;
using sj2324_5ehif_cooking_user.Application.Model;
using sj2324_5ehif_cooking_user.Webapi.Controllers;
using sj2324_5ehif_cooking_user.Webapi.Services;

namespace sj2324_5ehif_cooking_user.Test.UserTests;

public class AuthTest
{
    private readonly IConfiguration _mockConfiguration;
    private readonly UserContext _mockContext;
    private readonly JwtUtils _mockJwtUtils;
    private readonly ILogger<AuthController> _mockLogger;
    private readonly IPasswordUtils _mockPasswordUtils;

    public AuthTest()
    {
        var options = new DbContextOptionsBuilder<UserContext>()
            .UseInMemoryDatabase("TestDatabase")
            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            .Options;

        _mockContext = new UserContext(options);
        var faker = new Faker();

        _mockLogger = new Logger<AuthController>(new LoggerFactory());
        _mockJwtUtils = new JwtUtils(_mockConfiguration);
        _mockPasswordUtils = new PasswordUtils();

        _mockContext.Users.Add(new User
        (
            "testuser",
            firstname: faker.Name.FirstName(),
            lastname: faker.Name.LastName(),
            email: faker.Internet.Email(),
            password: "hashedpassword"
        ));

        _mockContext.SaveChanges();
    }

    [Fact]
    public async Task Register_ValidUser_ReturnsOk()
    {
        var controller = new AuthController(_mockContext, _mockLogger,
            _mockJwtUtils, _mockPasswordUtils);

        var faker = new Faker();
        var registerModel = new RegisterModel
        {
            Username = faker.Internet.UserName(),
            Lastname = faker.Name.LastName(),
            Firstname = faker.Name.FirstName(),
            Email = faker.Internet.Email(),
            Password = "hashedpassword"
        };

        var result = await controller.Register(registerModel) as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal("Registration successful", result.Value);
    }

    [Fact]
    public async Task Login_ValidCredentials_ReturnsToken()
    {
        var controller = new AuthController(_mockContext, _mockLogger,
            _mockJwtUtils, _mockPasswordUtils);

        var loginModel = new LoginModel
        {
            Username = _mockContext.Users.First().Username,
            Password = "hashedpassword"
        };

        var result = await controller.Login(loginModel) as OkObjectResult;

        Assert.NotNull(result);
        dynamic tokenResult = result.Value;
        Assert.NotNull(tokenResult.token);
    }
}