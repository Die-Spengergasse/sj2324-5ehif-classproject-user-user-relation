using sj2324_5ehif_cooking_user_relations.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user_relations.Application.Repository
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<(bool success, string message, T entity)> GetByIdAsync(object id);
        Task<(bool success, string message, List<T> entity)> GetAllAsync();
        Task<(bool success, string message)> InsertOneAsync(T entity);
        Task<(bool success, string message)> UpdateOneAsync(T entity);
        Task<(bool success, string message)> DeleteOneAsync(T entity);
    }
}
