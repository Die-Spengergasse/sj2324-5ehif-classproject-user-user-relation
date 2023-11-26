using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bogus;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using sj2324_5ehif_cooking_user.Webapi.Services;

namespace sj2324_5ehif_cooking_user.Test.ServiceTests;

public class JwtTest
{
    private readonly IConfiguration _configuration;
    private readonly IJwtUtils _jwtUtils;

    public JwtTest()
    {
        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string>("Jwt:Key", new Faker().Random.AlphaNumeric(32)),
                new KeyValuePair<string, string>("Jwt:Issuer", new Faker().Company.CompanyName()),
                new KeyValuePair<string, string>("Jwt:Audience", new Faker().Company.CompanyName()),
            }!)
            .Build();

        _jwtUtils = new JwtUtils(_configuration);
    }

    [Fact]
    public void GenerateJwtToken_ReturnsValidToken()
    {
        var username = "testuser";
        var jwtToken = _jwtUtils.GenerateJwtToken(username);
        Assert.NotNull(jwtToken);
        Assert.NotEmpty(jwtToken);
        Assert.True(ValidateToken(jwtToken));
    }

    [Fact]
    public void GenerateJwtToken_ContainsCorrectClaims()
    {
        var username = "testuser";
        var jwtToken = _jwtUtils.GenerateJwtToken(username);
        var token = new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);
        Assert.NotNull(token);
        Assert.Equal(_configuration["Jwt:Issuer"], token.Issuer);
        var claim = Assert.Single(token.Claims.ToList(), c => c.Value == username);
        Assert.NotNull(claim);
    }
    
    //helper method
    private bool ValidateToken(string jwtToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = _configuration["Jwt:Issuer"],
            ValidAudience = _configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        try
        {
            tokenHandler.ValidateToken(jwtToken, validationParameters, out _);
            return true;
        }
        catch
        {
            return false;
        }
    }
}