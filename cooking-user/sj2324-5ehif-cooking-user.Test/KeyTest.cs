using System.Net.Mime;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using sj2324_5ehif_cooking_user.Application.Model;
using Key = sj2324_5ehif_cooking_user.Application.Model.Key;

namespace sj2324_5ehif_cooking_user.Test;

public class KeyTest
{
    [Fact]
    public void TestKeyStructure()
    {
        var prefix = "Test";
        var length = 12;
        TestKey testKey = new TestKey(prefix: prefix, length: length);
        Assert.True(testKey._value.Substring(0, prefix.Length) == prefix);

        Assert.True(testKey._value.Substring(prefix.Length - 1, length).Length == length);
    }

    [Fact]
    public void TestKeyCheck()
    {
        var prefix = "Test";
        var length = 12;
        TestKey testKey = new TestKey(prefix: prefix, length: length);
        Assert.True(testKey.CheckKey(testKey));
    }
}