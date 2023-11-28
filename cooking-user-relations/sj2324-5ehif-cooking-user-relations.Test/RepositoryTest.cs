using Assert = NUnit.Framework.Assert;
using Bogus;
using sj2324_5ehif_cooking_user_relations.Application.Model;
using sj2324_5ehif_cooking_user_relations.Application.Repository;

namespace sj2324_5ehif_cooking_user_relations.Test
{
    public class UserRepositoryTests : DatabaseTest
    {
        private readonly UserRepository _userRepository;
        public UserRepositoryTests()
        {

            _userRepository = new UserRepository(_context);

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

        [Fact]
        public async Task AddUserTest()
        {
            // Prep
            await _userRepository.DeleteAllAsync();

            var user1 = GenerateUser();
            // Act
            var result = await _userRepository.InsertOneAsync(user1);

            // Check
            var check = await _userRepository.GetByIdAsync(user1.Key);

            // Assert
            Assert.IsTrue(result.success);
            Assert.IsTrue(check.success);
            Assert.AreEqual(check.entity.Key, user1.Key);
        }
        [Fact]
        public async Task GetByKeyTest()
        {
            // Prep
            await _userRepository.DeleteAllAsync();

            var user1 = GenerateUser();
            var key = user1.Key;

            await _userRepository.InsertOneAsync(user1);

            // Act
            var result = await _userRepository.GetByIdAsync(key);

            // Assert
            Assert.AreEqual(key, result.entity.Key);
            Assert.IsTrue(result.success);
            Assert.IsNotNull(result.entity);

        }

        [Fact]
        public async Task GetAllUsersTest()
        {
            // Prep
            await _userRepository.DeleteAllAsync();

            var user1 = GenerateUser();
            var user2 = GenerateUser();

            await _userRepository.InsertOneAsync(user1);
            await _userRepository.InsertOneAsync(user2);

            // Act
            var result = await _userRepository.GetAllAsync();

            // Assert
            Assert.IsTrue(result.success);
            Assert.IsNotNull(result.entity);
            Assert.AreEqual(2, result.entity.Count);

            await _userRepository.DeleteAllAsync();


        }

        [Fact]
        public async Task UpdateUserTest()
        {
            // Prep
            await _userRepository.DeleteAllAsync();
            var user1 = GenerateUser();
            await _userRepository.InsertOneAsync(user1);

            user1.Name = "Xenox Cardio";
            // Act
            var result = await _userRepository.UpdateOneAsync(user1);

            // Check
            var updated_user = await _userRepository.GetByIdAsync(user1.Key);

            // Assert
            Assert.IsTrue(result.success);
            Assert.AreEqual(user1.Name, updated_user.entity.Name);
        }

        [Fact]
        public async Task DeleteUserTest()
        {
            // Prep
            await _userRepository.DeleteAllAsync();
            var user1 = GenerateUser();
            var insert_res = await _userRepository.InsertOneAsync(user1);
            Assert.IsTrue(insert_res.success, "Insertion failed");

            var check_res = await _userRepository.GetByIdAsync(user1.Key);
            if (check_res.success)
            {
                // Act
                var result = await _userRepository.DeleteOneAsync(user1);
                var check = await _userRepository.GetByIdAsync(user1.Key);

                // Assert
                Assert.IsTrue(result.success, "Deletion failed");
                Assert.IsFalse(check.success, "User still exists after deletion");
            }
            else
            {
                Assert.Fail("User not found after insertion");
            }
        }

    }
}
