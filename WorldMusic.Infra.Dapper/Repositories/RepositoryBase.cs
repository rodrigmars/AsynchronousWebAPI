using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WorldMusic.Domain.Interfaces.Repositories;
using WorldMusic.Infra.Dapper.Interface;

namespace WorldMusic.Infra.Dapper.Repositories
{
    public class RepositoryBase<TEntity>: IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly IDapperDbContext _context;

        public RepositoryBase(IDapperDbContext context)
        {
            _context = context;
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
            return _context.Connection(conn =>
            {
                using (var c = conn)
                {
                    return (c.Execute(query, param, commandType: command) > 0);
                }
            });
        }

        public IEnumerable<TEntity> GetAll(string query, CommandType command = CommandType.Text)
        {
            return _context.Connection(conn =>
            {
                using (var c = conn) return c.Query<TEntity>(query, commandType: command);
            });
        }

        public TEntity GetById(string query, dynamic param, CommandType command = CommandType.Text)
        {
            return _context.Connection(conn =>
            {
                using (var c = conn)
                {
                    return c.Query<TEntity>(query).FirstOrDefault();
                }
            });
        }

        public bool Update(string query, object param, CommandType command = CommandType.Text)
        {
            return _context.Connection(conn =>
            {
                using (var c = conn)
                {
                    return (c.Execute(query, param, commandType: command) > 0);
                }
            });
        }

        public bool Remove(string query, object param, CommandType command = CommandType.Text)
        {
            return _context.Connection(conn =>
            {
                using (var c = conn)
                {
                    return (c.Execute(query, param, commandType: command) > 0);
                }
            });
        }
    }
}
