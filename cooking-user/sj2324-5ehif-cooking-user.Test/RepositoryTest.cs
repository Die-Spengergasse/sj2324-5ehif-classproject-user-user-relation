using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using sj2324_5ehif_cooking_user.Application.Model;
using sj2324_5ehif_cooking_user.Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using sj2324_5ehif_cooking_user.Application.Infrastructure;
using Bogus;
using Assert = NUnit.Framework.Assert;
using System.Text;

namespace sj2324_5ehif_cooking_user.Test
{

    public class UserRepositoryTests : DatabaseTest
    {
        private readonly UserRepository _userRepository;
        public UserRepositoryTests()
        {

            _userRepository = new UserRepository(_context);

        }

        public static string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        public static User GenerateUser()
        {
            var faker = new Faker<User>()
                .CustomInstantiator(f => new User(
                        username: f.Person.UserName,
                        firstname: f.Person.FirstName,
                        lastname: f.Person.LastName,
                        email: f.Person.Email,
                        password: CreatePassword(8)

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

            user1.Email = "LuisStinkt@gmail.com";
            // Act
            var result = await _userRepository.UpdateOneAsync(user1);

            // Check
            var updated_user = await _userRepository.GetByIdAsync(user1.Key);

            // Assert
            Assert.IsTrue(result.success);
            Assert.AreEqual(user1.Email, updated_user.entity.Email);
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