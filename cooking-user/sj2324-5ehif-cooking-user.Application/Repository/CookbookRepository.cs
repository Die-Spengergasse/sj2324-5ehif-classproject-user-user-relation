using Microsoft.EntityFrameworkCore;
using sj2324_5ehif_cooking_user.Application.Infrastructure;
using sj2324_5ehif_cooking_user.Application.Model;

namespace sj2324_5ehif_cooking_user.Application.Repository;

public class CookbookRepository : IRepository<Cookbook>
{
    private readonly Repository<Cookbook> _repository;

    public CookbookRepository(UserContext context)
    {
        _repository = new Repository<Cookbook>(context);
    }

    public Task<(bool success, string message, Cookbook entity)> GetByIdAsync(string Key)
    {
        return _repository.GetByIdAsync(Key);
    }


    public Task<(bool success, string message, List<Cookbook> entity)> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<(bool success, string message)> InsertOneAsync(Cookbook entity)
    {
        return _repository.InsertOneAsync(entity);
    }

    public Task<(bool success, string message)> UpdateOneAsync(Cookbook entity)
    {
        return _repository.UpdateOneAsync(entity);
    }

    public Task<(bool success, string message)> DeleteOneAsync(string key)
    {
        return _repository.DeleteOneAsync(key);
    }
}