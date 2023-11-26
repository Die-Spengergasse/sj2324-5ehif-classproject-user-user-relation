using System.Security.Claims;
using AutoMapper;
using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using sj2324_5ehif_cooking_user.Application.DTO;
using sj2324_5ehif_cooking_user.Application.Infrastructure;
using sj2324_5ehif_cooking_user.Application.Model;
using sj2324_5ehif_cooking_user.Webapi.Controllers;
using sj2324_5ehif_cooking_user.Webapi.Services;

namespace sj2324_5ehif_cooking_user.Test.UserTests;

public class UserTest
{
    private readonly UserContext _mockContext;
    private readonly ILogger<UserController> _mockLogger;
    private readonly IMapper _mockMapper;
    private readonly IPasswordUtils _mockPasswordUtils;

    public UserTest()
    {
        var options = new DbContextOptionsBuilder<UserContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        _mockContext = new UserContext(options);
        var faker = new Faker();

        _mockLogger = new Logger<UserController>(new LoggerFactory());
        _mockPasswordUtils = new PasswordUtils();
        _mockMapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<PreferenceDto, Preference>()));

        _mockContext.Users.AddRange(new User
        (
            faker.Internet.UserName(),
            firstname: faker.Name.FirstName(),
            lastname: faker.Name.LastName(),
            email: faker.Internet.Email(),
            password: "hashedpassword"
        ));

        _mockContext.SaveChanges();
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
}