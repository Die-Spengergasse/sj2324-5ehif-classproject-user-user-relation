using sj2324_5ehif_cooking_user.Webapi.Services;

namespace sj2324_5ehif_cooking_user.Test.ServiceTests;

public class PasswordTest
{
    private readonly PasswordUtils _passwordUtils = new();

    [Fact]
    public void HashPassword_ReturnsValidHash()
    {
        var password = "MySecurePassword";
        var hashedPassword = _passwordUtils.HashPassword(password);
        Assert.NotNull(hashedPassword);
        Assert.NotEmpty(hashedPassword);
    }

    [Fact]
    public void HashPassword_ReturnsSameHashForSameInput()
    {
        var password = "MySecurePassword";
        var hashedPassword1 = _passwordUtils.HashPassword(password);
        var hashedPassword2 = _passwordUtils.HashPassword(password);
        Assert.Equal(hashedPassword1, hashedPassword2);
    }

    [Fact]
    public void HashPassword_DifferentPasswordsProduceDifferentHashes()
    {
        var password1 = "Password123";
        var password2 = "Password1234";
        var hashedPassword1 = _passwordUtils.HashPassword(password1);
        var hashedPassword2 = _passwordUtils.HashPassword(password2);
        Assert.NotEqual(hashedPassword1, hashedPassword2);
    }
}