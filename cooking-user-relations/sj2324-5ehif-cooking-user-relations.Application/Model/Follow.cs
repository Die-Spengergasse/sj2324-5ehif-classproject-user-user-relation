using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user_relations.Application.Model
{
    public class Follow : IEntity
    {
        [Required] [Key] public string Key { get; set; }
        [NotMapped]
        public FollowKey ObjectKey
        {
            get => new(Key);
            set => Key = value.Value;
        }

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
            ObjectKey = new FollowKey();
            User = user;
            Follower = follower;
        }

    }
}
