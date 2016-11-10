using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WorldMusic.Domain.Interfaces.Repositories;
using WorldMusic.Infra.Dapper.Interface;

namespace WorldMusic.Infra.Dapper.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected IDapperDbContext _context;

        public RepositoryBase(IDapperDbContext context)
        {
            _context = context;
        }

        public void Close()
        {
            _context.Closed();

            _context.MonitoringConnection();
        }

        public async Task<bool> AddAsync(string query, object param)
        {
            return await _context.ConnectionAsync(async connAsync =>
            {
                using (var conn = connAsync) return (await conn.ExecuteAsync(query, param) > 0);
            });
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(string query)
        {
            return await _context.ConnectionAsync(async connAsync =>
            {
                using (var conn = connAsync) return await conn.QueryAsync<TEntity>(query);
            });
        }

        public async Task<TEntity> GetByIdAsync(string query, dynamic param)
        {
            return await _context.ConnectionAsync(async connAsync =>
            {
                using (var conn = connAsync)
                {
                    var result = await conn.QueryAsync<TEntity>(query);

                    return result.FirstOrDefault();
                }
            });
        }

        public async Task<bool> UpdateAsync(string query, object param)
        {
            return await _context.ConnectionAsync(async connAsync =>
            {
                using (var conn = connAsync)
                {
                    return (await conn.ExecuteAsync(query, param) > 0);
                }
            });
        }

        public async Task<bool> RemoveAsync(string query, object param)
        {
            return await _context.ConnectionAsync(async connAsync =>
            {
                using (var conn = connAsync)
                {
                    return (await conn.ExecuteAsync(query, param) > 0);
                }
            });
        }

        public bool Add(string query, object param, CommandType command = CommandType.Text)
        {
            return _context.Connection.Execute(query, param, commandType: command) > 0;
        }

        public IEnumerable<TEntity> GetAll(string query, CommandType command = CommandType.Text)
        {
            return _context.Connection.Query<TEntity>(query, commandType: command);
        }

        public TEntity GetById(string query, dynamic param, CommandType command = CommandType.Text)
        {
            return _context.Connection.Query<TEntity>(query).FirstOrDefault();
        }

        public bool Update(string query, object param, CommandType command = CommandType.Text)
        {
            return _context.Connection.Execute(query, param, commandType: command) > 0;
        }

        public bool Remove(string query, object param, CommandType command = CommandType.Text)
        {
            return _context.Connection.Execute(query, param, commandType: command) > 0;
        }
    }
}
