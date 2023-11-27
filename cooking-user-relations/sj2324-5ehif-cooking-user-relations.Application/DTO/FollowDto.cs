using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user_relations.Application.DTO
{
    public class FollowDto
    {
        public string FollowKey { get; set; }
        public UserDto Follower { get; set; }
        public UserDto Followed { get; set; }
    }
}
