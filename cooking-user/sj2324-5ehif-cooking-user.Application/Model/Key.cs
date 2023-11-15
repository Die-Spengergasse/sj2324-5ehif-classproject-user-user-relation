using System.Text;
using SimpleISO7064;

namespace sj2324_5ehif_cooking_user.Application.Model;

public abstract class Key
{
    public String Prefix { get; }
    public int Length { get; }
    public  string Value { get; }

    protected Key(string prefix, int length)
    {
        Prefix = prefix;
        Length = length;
        Value = GenerateKey();
    }

    protected Key(string value, string prefix, int length)
    {
        Prefix = prefix;
        Length = length;
        Value = value;
        
    }
    

    private string GetRandomPart()
    {
        Random rnd = new Random();
        StringBuilder sb = new StringBuilder(Length);
        string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        int counter = 0;
        for (int i = 0; i < Length; i++)
        {
            sb.Append(chars[rnd.Next(chars.Length)]);
        }

        return sb.ToString();
    }

    public string GenerateKey()
    {
        string randomPart = GetRandomPart();
        string key = Prefix + randomPart;
        string a = new Iso7064Factory().GetMod37Radix2().ComputeCheckDigit(key);
        return key + a;
    }

    public static bool CheckPrefix(string value, string prefix)
    {
        if (value.Length < prefix.Length)
        {
            return false;
        }

        return value.Substring(0, prefix.Length ) == prefix;
    }

    public static bool CheckKey(string value, string? prefix=null)
    {
        var check = prefix == null ? true : CheckPrefix(value: value, prefix: prefix);
        return new Iso7064Factory().GetMod37Radix2()
            .IsValid(value) & check;
    }
}