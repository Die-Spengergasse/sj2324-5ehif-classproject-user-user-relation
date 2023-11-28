using sj2324_5ehif_cooking_user_relations.Application.Infrastructure;
using sj2324_5ehif_cooking_user_relations.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user_relations.Application.Repository
{
    public class FollowRepository : Repository<Follow>

    {

        public FollowRepository(UserRelationsContext context) : base(context)

        {

        }

    }
}
