using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldMusic.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<bool> AddAsync(string query, object param);

        Task<IEnumerable<TEntity>> GetAllAsync(string query);

        Task<TEntity> GetByIdAsync(string query, dynamic param);

        Task<bool> UpdateAsync(string query, object param);

        Task<bool> RemoveAsync(string query, object param);

        bool Add(string query, object param);

        IEnumerable<TEntity> GetAll(string query);

        TEntity GetById(string query, dynamic param);

        bool Update(string query, object param);

        bool Remove(string query, object param);
    }
}