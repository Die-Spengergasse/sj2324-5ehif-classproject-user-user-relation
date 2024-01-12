using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sj2324_5ehif_cooking_user_relations.Application.DTO.User;

namespace sj2324_5ehif_cooking_user_relations.Application.DTO.Follow
{
    public class FollowDto
    {
        public string Key { get; set; }
        public UserDto Follower { get; set; }
        public UserDto User { get; set; }
    }
}
