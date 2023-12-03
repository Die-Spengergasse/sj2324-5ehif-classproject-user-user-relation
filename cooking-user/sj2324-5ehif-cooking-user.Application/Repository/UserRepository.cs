using Microsoft.EntityFrameworkCore;
using sj2324_5ehif_cooking_user.Application.Infrastructure;
using sj2324_5ehif_cooking_user.Application.Model;

namespace sj2324_5ehif_cooking_user.Application.Repository;

public class UserRepository : IRepository<User>
{
    private readonly Repository<User> _repository;

    public UserRepository(UserContext context)
    {
        _repository = new Repository<User>(context);
    }

    public Task<(bool success, string message, User entity)> GetByIdAsync(string Key)
    {
        return _repository.GetByIdAsync(Key);
    }


    public Task<(bool success, string message, List<User> entity)> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<(bool success, string message)> InsertOneAsync(User entity)
    {
        return _repository.InsertOneAsync(entity);
    }

    public Task<(bool success, string message)> UpdateOneAsync(User entity)
    {
        return _repository.UpdateOneAsync(entity);
    }

    public Task<(bool success, string message)> DeleteOneAsync(string key)
    {
        return _repository.DeleteOneAsync(key);
    }
}