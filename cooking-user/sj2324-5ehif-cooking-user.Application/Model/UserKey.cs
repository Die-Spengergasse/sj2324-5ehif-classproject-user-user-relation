using System.Net.NetworkInformation;

namespace sj2324_5ehif_cooking_user.Application.Model;

public class UserKey : Key
{
    public UserKey() : base(prefix: "USR", length: 14)
    {
    }
}