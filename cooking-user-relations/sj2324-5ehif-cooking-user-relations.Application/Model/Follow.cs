using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user_relations.Application.Model
{
    public class Follow
    {
        [Key] public long Id { get; private set; }
        [Required] public FollowKey Key { get; }

        [Required] public User User { get; }
        [Required] public User Follower { get; }

#pragma warning disable CS8618
        protected Follow()
        {
        }

        public Follow(User user, User follower)
        {
            if (user == null || follower == null)
            {
                throw new ArgumentNullException(user == null ? nameof(user) : nameof(follower));
            }

            if (user == follower)
            {
                throw new ArgumentException("A user cannot follow themselves.");
            }
            Key = new FollowKey();
            User = user;
            Follower = follower;
        }

    }
}
