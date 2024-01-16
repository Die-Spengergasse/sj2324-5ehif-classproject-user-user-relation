using Microsoft.EntityFrameworkCore;
using sj2324_5ehif_cooking_user.Application.Infrastructure;

namespace sj2324_5ehif_cooking_user.Application.Repository;

public interface IRepository<T> where T : class
{
    Task<(bool success, string message, T entity)> GetByKeyAsync(string key);
    Task<(bool success, string message, List<T> entity)> GetAllAsync();
    Task<(bool success, string message)> InsertOneAsync(T entity);
    Task<(bool success, string message)> UpdateOneAsync(T entity);
    Task<(bool success, string message)> DeleteOneAsync(string key);
    Task<(bool success, string message)> SaveChangesAsync();
}

public class Repository<T> : IRepository<T> where T : class
{
    private readonly UserContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(UserContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<T>();
    }

    public async Task<(bool success, string message, T? entity)> GetByKeyAsync(string key)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));

        try
        {
            var entity = await _dbSet.SingleOrDefaultAsync(e => EF.Property<string>(e, "Key") == key);

            return entity != null ? (true, "Entity found", entity) : (false, "Entity not found", null);
        }
        catch (Exception e)
        {
            return (false, e.InnerException?.Message ?? e.Message, null);
        }
    }

    public virtual async Task<(bool success, string message, List<T>? entity)> GetAllAsync()
    {
        try
        {
            // avoid hitting the database if the entity is already loaded
            var entity = await _dbSet.ToListAsync();
            return (true, string.Empty, entity);
        }
        catch (DbUpdateException e)
        {
            return (false, e.InnerException?.Message ?? e.Message, null);
        }
    }

    public async Task<(bool success, string message)> InsertOneAsync(T entity)
    {
        var entry = _context.Entry(entity);

        if (entry.State == EntityState.Detached)
            _dbSet.Add(entity);
        else
            return (false, "Entity is already in the database.");

        try
        {
            await _context.SaveChangesAsync();
            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.InnerException?.Message ?? e.Message);
        }
    }

    public async Task<(bool success, string message)> UpdateOneAsync(T entity)
    {
        _dbSet.Update(entity);
        try
        {
            await _context.SaveChangesAsync();
            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.InnerException?.Message ?? e.Message);
        }
    }

    public async Task<(bool success, string message)> DeleteOneAsync(string key)
    {
        var result = await GetByKeyAsync(key);
        if (!result.success) return (false, "Entity not found");

        _dbSet.Remove(result.entity);
        try
        {
            await _context.SaveChangesAsync();
            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.InnerException?.Message ?? e.Message);
        }
    }

    public async Task<(bool success, string message)> SaveChangesAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.InnerException?.Message ?? e.Message);
        }
    }
}