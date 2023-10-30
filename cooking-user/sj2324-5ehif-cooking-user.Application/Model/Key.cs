using System.Diagnostics;
using System.Text;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Checksum;
using SimpleISO7064;

namespace sj2324_5ehif_cooking_user.Application.Model;

public abstract class Key
{
    public String _prefix { get; }
    private int _length { get; }
    public readonly string _value;

    protected Key(string prefix, int length)
    {
        _prefix = prefix;
        _length = length;
        _value = GenerateKey();
    }

    private string GetRandomPart(int length)
    {
        Random rnd = new Random();
        StringBuilder sb = new StringBuilder(length);
        string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        int counter = 0;
        for (int i = 0; i < _length; i++)
        {
            sb.Append(chars[rnd.Next(chars.Length)]);
        }

        return sb.ToString();
    }

    public string GenerateKey()
    {
        string randomPart = GetRandomPart(_length);
        string key = _prefix + randomPart;
        string a = new Iso7064Factory().GetMod37Radix2().ComputeCheckDigit(key);
        return key + a;
    }

    public bool CheckKey(Key key)
    {
        return new Iso7064Factory().GetMod37Radix2()
            .IsValid(key._value);
    }
}