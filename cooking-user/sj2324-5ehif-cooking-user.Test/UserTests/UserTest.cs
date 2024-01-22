using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
    }
}