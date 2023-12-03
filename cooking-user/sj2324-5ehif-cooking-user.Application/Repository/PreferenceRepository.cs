using Microsoft.EntityFrameworkCore;
using sj2324_5ehif_cooking_user.Application.Model;

namespace sj2324_5ehif_cooking_user.Application.Repository;

public class PreferenceRepository : Repository<Preference>
{
    public PreferenceRepository(DbContext context) : base(context)
    {
    }
}