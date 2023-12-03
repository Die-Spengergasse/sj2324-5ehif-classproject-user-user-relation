using sj2324_5ehif_cooking_user.Application.Infrastructure;
using sj2324_5ehif_cooking_user.Application.Model;

namespace sj2324_5ehif_cooking_user.Application.Repository;

public class UserRepository : Repository<User>
{
    public UserRepository(UserContext context) : base(context)
    {
    }
}