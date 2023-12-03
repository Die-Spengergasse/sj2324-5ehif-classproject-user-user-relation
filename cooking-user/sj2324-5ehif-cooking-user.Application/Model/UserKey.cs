namespace sj2324_5ehif_cooking_user.Application.Model;

public class UserKey : Key
{
    public UserKey() : base(prefix: "USR", length: 14)
    {
    }
    public UserKey(string value) : base(value:value,prefix:"USR", length:14)
    {
    }
} 