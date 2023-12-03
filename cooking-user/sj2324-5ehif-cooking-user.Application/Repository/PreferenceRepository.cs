using Microsoft.EntityFrameworkCore;
using sj2324_5ehif_cooking_user.Application.Infrastructure;
using sj2324_5ehif_cooking_user.Application.Model;

namespace sj2324_5ehif_cooking_user.Application.Repository;

public class PreferenceRepository : IRepository<Preference>
{
    private readonly Repository<Preference> _repository;

    public PreferenceRepository(UserContext context)
    {
        _repository = new Repository<Preference>(context);
    }

    public Task<(bool success, string message, Preference entity)> GetByIdAsync(string Key)
    {
        return _repository.GetByIdAsync(Key);
    }


    public Task<(bool success, string message, List<Preference> entity)> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<(bool success, string message)> InsertOneAsync(Preference entity)
    {
        return _repository.InsertOneAsync(entity);
    }

    public Task<(bool success, string message)> UpdateOneAsync(Preference entity)
    {
        return _repository.UpdateOneAsync(entity);
    }

    public Task<(bool success, string message)> DeleteOneAsync(string key)
    {
        return _repository.DeleteOneAsync(key);
    }
}