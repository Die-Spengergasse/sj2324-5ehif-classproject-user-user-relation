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
        Assert.True(testKey.Value.Substring(0, prefix.Length) == prefix);

        Assert.True(testKey.Value.Substring(prefix.Length - 1, length).Length == length);
    }

    [Fact]
    public void TestKeyCheck()
    {
        var prefix = "Test";
        var length = 12;
        TestKey testKey = new TestKey(prefix: prefix, length: length);
        Assert.True(TestKey.CheckKey(value:testKey.Value,prefix: prefix));
    }
    
    [Fact]
    public void TestWrongKeyCheckPrefix()
    {
        Assert.False(TestKey.CheckKey(value:"assssssssssss",prefix:"adadsssdsdddddddd"));
    }
    [Fact]
    public void TestWrongKeyCheck()
    {
        Assert.False(TestKey.CheckKey(value:"90990000000asas"));
    }


    
}