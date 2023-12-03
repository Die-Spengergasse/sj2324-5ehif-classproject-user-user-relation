using Microsoft.EntityFrameworkCore;
using sj2324_5ehif_cooking_user.Application.Infrastructure;
using sj2324_5ehif_cooking_user.Application.Model;

namespace sj2324_5ehif_cooking_user.Application.Repository;

public class RecipeRepository : IRepository<Recipe>
{
    private readonly Repository<Recipe> _repository;

    public RecipeRepository(UserContext context)
    {
        _repository = new Repository<Recipe>(context);
    }

    public Task<(bool success, string message, Recipe entity)> GetByIdAsync(string Key)
    {
        return _repository.GetByIdAsync(Key);
    }

    public Task<(bool success, string message, List<Recipe> entity)> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<(bool success, string message)> InsertOneAsync(Recipe entity)
    {
        return _repository.InsertOneAsync(entity);
    }

    public Task<(bool success, string message)> UpdateOneAsync(Recipe entity)
    {
        return _repository.UpdateOneAsync(entity);
    }

    public Task<(bool success, string message)> DeleteOneAsync(string key)
    {
        return _repository.DeleteOneAsync(key);
    }
}