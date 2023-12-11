using Assert = NUnit.Framework.Assert;
using Bogus;
using sj2324_5ehif_cooking_user_relations.Application.Model;
using sj2324_5ehif_cooking_user_relations.Application.Repository;
using Microsoft.EntityFrameworkCore;


namespace sj2324_5ehif_cooking_user_relations.Test
{
    public class UserRepositoryTests : DatabaseTest
    {
        private readonly Repository<User> _userRepository;
        public UserRepositoryTests()
        {
            _userRepository = new Repository<User>(_context);

        }
        public static User GenerateUser()
        {
            var faker = new Faker<User>()
                .CustomInstantiator(f => new User(
                        name: f.Person.FullName,
                        key: Guid.NewGuid().ToString("N")

                    )
                );
            return faker.Generate();
        }

        public async void SetupContext()
        {
            _context.Set<User>().RemoveRange(await _context.Set<User>().ToListAsync());
            await _context.SaveChangesAsync();
        }

        public async Task<User> AddUser()
        {
            var user = GenerateUser();
            await _context.Set<User>().AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        [Fact]
        public async Task AddUserTest()
        {
            // Prep
            SetupContext();
            var user1 = GenerateUser();

            // Act
            var result = await _userRepository.InsertOneAsync(user1);

            // Check
            var check = await _context.Set<User>().SingleOrDefaultAsync(e => e.Key == user1.Key); 

            // Assert
            Assert.IsTrue(result.success);
            Assert.IsNotNull(check);
            Assert.AreEqual(check?.Key, user1.Key);

            // Clean
            SetupContext();
        }
        [Fact]
        public async Task GetByKeyTest()
        {
            // Prep
            SetupContext();
            var user = AddUser().Result;

            // Act
            var result = await _userRepository.GetByIdAsync(user.Key);

            // Assert
            Assert.AreEqual(user.Key, result.entity.Key);
            Assert.IsTrue(result.success);
            Assert.IsNotNull(result.entity);

        }

        [Fact]
        public async Task GetAllUsersTest()
        {
            // Prep
            SetupContext();
            var user1 = AddUser();
            var user2 = AddUser();
            
            // Act
            var result = await _userRepository.GetAllAsync();

            // Assert
            Assert.IsTrue(result.success);
            Assert.IsNotNull(result.entity);
            Assert.AreEqual(2, result.entity.Count);

            // Clean
            SetupContext();
        }

        [Fact]
        public async Task UpdateUserTest()
        {
            // Prep
            SetupContext();
            var user = AddUser().Result;

            user.Name = "Xenox Cardio";
            // Act
            var result = await _userRepository.UpdateOneAsync(user);

            // Check
            var updated_user = await _context.Set<User>().SingleOrDefaultAsync(e => e.Key == user.Key);

            // Assert
            Assert.IsTrue(result.success);
            Assert.AreEqual(user.Name, updated_user?.Name);

            // Clean
            SetupContext();
        }

        [Fact]
        public async Task DeleteUserTest()
        {
            // Prep
            SetupContext();
            var user = AddUser().Result;
            var check_user = await _context.Set<User>().SingleOrDefaultAsync(e => e.Key == user.Key);

            // Check
            if (check_user is not null)
            {
                // Act
                var result = await _userRepository.DeleteOneAsync(user.Key);

                var check = await _context.Set<User>().SingleOrDefaultAsync(e => e.Key == user.Key);

                // Assert
                Assert.IsTrue(result.success, "Deletion failed");
                Assert.IsNull(check, "User still exists after deletion");
                Assert.AreEqual(0, _context.Set<User>().Count(), "User was not deleted");
            }
            else
            {
                Assert.Fail("User not found after insertion");
            }
        }

    }
}
