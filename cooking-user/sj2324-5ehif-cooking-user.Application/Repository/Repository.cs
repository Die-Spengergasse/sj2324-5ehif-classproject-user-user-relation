using Microsoft.EntityFrameworkCore;
using sj2324_5ehif_cooking_user.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user.Application.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    
    {
        protected readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public async Task<(bool success, string message, T? entity)> GetByIdAsync(object id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            try
            {
                var entity = await _dbSet.SingleOrDefaultAsync(e => e.Key == id);

                if (entity == null)
                {
                    return (false, $"No entity with ID {id} found.", null);
                }
                else
                {
                    return (true, string.Empty, entity);
                }
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
                return  (true, string.Empty, entity);
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
            {
                _dbSet.Add(entity);
            }
            else
            {
                return (false, "Entity is already in the database.");
            }

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

        public async Task<(bool success, string message)> DeleteOneAsync(T entity)
        {
            _dbSet.Remove(entity);
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

        public async Task<(bool success, string message)> DeleteAllAsync()
        {

            _context.Set<T>().RemoveRange(await _context.Set<T>().ToListAsync());

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

}
