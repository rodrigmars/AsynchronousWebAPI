using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WorldMusic.Domain.Interfaces.Repositories;
using WorldMusic.Infra.Dapper.Interface;


//reference: https://github.com/whisperdancer/AspNet.Identity.Dapper/blob/master/AspNet.Identity.Dapper/DbManager.cs

namespace WorldMusic.Infra.Dapper.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected IDapperDbContext _context;

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
            return _context.Connection_(conn =>
            {
                using (var c = conn)
                {
                    return (c.Execute(query, param, commandType: command) > 0);
                }
            });
        }

        public IEnumerable<TEntity> GetAll(string query, CommandType command = CommandType.Text)
        {

            return _context.Connection.Query<TEntity>(query, commandType: command);


            //return _context.Connection(conn =>
            //{
            //    var obj = new List<TEntity>();
            //    using (var c = conn)
            //    {
            //        obj.AddRange(c.Query<TEntity>(query, commandType: command));
            //    }

            //    System.Diagnostics.Trace.WriteLine(obj.Count, ">>>>>> Método GetAll() executado, total de registros = ");
            //    System.Diagnostics.Trace.WriteLine(conn.State, ">>>>>> CONEXÃO STATE = ");

            //    return obj;
            //});
        }

        public TEntity GetById(string query, dynamic param, CommandType command = CommandType.Text)
        {
            return _context.Connection_(conn =>
            {
                using (var c = conn)
                {
                    return c.Query<TEntity>(query).FirstOrDefault();
                }
            });
        }

        public bool Update(string query, object param, CommandType command = CommandType.Text)
        {
            return _context.Connection_(conn =>
            {
                using (var c = conn)
                {
                    return (c.Execute(query, param, commandType: command) > 0);
                }
            });
        }

        public bool Remove(string query, object param, CommandType command = CommandType.Text)
        {
            return _context.Connection_(conn =>
            {
                using (var c = conn)
                {
                    return (c.Execute(query, param, commandType: command) > 0);
                }
            });
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }
        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_context.Connection != null) _context.Connection.Dispose();
                }

                _context.Disposed(_disposed = true);
            }
        }

        ~RepositoryBase()
        {
            Dispose(false);
        }
    }
}
