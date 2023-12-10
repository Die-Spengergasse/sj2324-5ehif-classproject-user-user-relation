using sj2324_5ehif_cooking_user_relations.Application.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user_relations.Application.DTO.Follow
{
    public class AddFollowDto
    {
        public UserDto Follower { get; set; }
        public UserDto User { get; set; }
    }
}
