using System.Threading.Tasks;
using sj2324_5ehif_cooking_user_relations.Application.DTO.Follow;
using sj2324_5ehif_cooking_user_relations.Application.DTO.User;
using sj2324_5ehif_cooking_user_relations.Application.Model;
using sj2324_5ehif_cooking_user_relations.Application.Repository;

namespace sj2324_5ehif_cooking_user_relations.Application.Services
{
    public interface IFollowService
    {
        Task<bool> AddFollower(AddFollowDto addFollowDto);
        Task<IEnumerable<FollowDto>> GetAllFollowsAsync();
    }
    
    public class FollowService : IFollowService
    {
        private readonly IRepository<Follow> _followRepository;
        private readonly IRepository<User> _userRepository;

        public FollowService(IRepository<Follow> followRepository, IRepository<User> userRepository)
        {
            _followRepository = followRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> AddFollower(AddFollowDto addFollowDto)
        {
            // Validate the follower and the user exist
            var follower = _userRepository.GetByIdAsync(addFollowDto.User.Key).Result.entity;
            var userToFollow = _userRepository.GetByIdAsync(addFollowDto.Follower.Key).Result.entity;

            if (follower == null || userToFollow == null)
            {
                // Handle the case where either the follower or the user do not exist.
                throw new InvalidOperationException("Follower or User not found.");
            }

            var existingUserFollow = (await _followRepository.GetAllAsync()).entity.SingleOrDefault(
                u => u.User.Key == addFollowDto.User.Key && u.Follower.Key == userToFollow.Key);
        
            if (existingUserFollow is not null)
            {
                throw new InvalidOperationException("This User is already a Follower.");
            }

            // Create a new follow relationship
            var follow = new Follow(userToFollow, follower);

            // Save the new follow relationship
            await _followRepository.InsertOneAsync(follow);
            await _followRepository.SaveChangesAsync();
            
            return true; 
        }
        
        public async Task<IEnumerable<FollowDto>> GetAllFollowsAsync()
        {
            var follows = (await _followRepository.GetAllAsync()).entity;
            
            var followDtos = follows.Select(f => new FollowDto 
            {
                // Map the properties from the Follow entity to the FollowDto
                Key = f.Key,
                Follower = new UserDto
                {
                    Key = f.Follower.Key,
                    Name = f.Follower.Name

                },
                User = new UserDto
                {
                    Key = f.User.Key,
                    Name = f.User.Name

                }
            });
           
            return followDtos;
        }
    }
}
