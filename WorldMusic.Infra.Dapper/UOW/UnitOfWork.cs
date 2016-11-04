using Dapper;
using System;
using System.Data;
using System.Diagnostics;
using WorldMusic.Domain.Interfaces.Repositories;
using WorldMusic.Infra.Dapper.Interface;

namespace WorldMusic.Infra.Dapper.UOW
{
    public class UnitOfWork : IUnitOfWork
    {

        //http://www.kspace.pt/posts/UnitOfWork

        private IDapperDbContext _context;

        private IDbTransaction _transaction { get; set; }
        private bool _disposed;

    
        public UnitOfWork(IDapperDbContext contex)
        {
            _context = contex;
        }

        public void BeginTransaction()
        {
            if (_transaction == null) _transaction = _context.ConnectionTransaction.BeginTransaction();

        }

        public void Commit()
        {
            if (_transaction != null) _transaction.Commit();
        }

        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }
        }

        public int Execute(string query, object param = null, CommandType commandType = CommandType.Text)
        {
            return _context.ConnectionTransaction.Execute(query, param, _transaction, commandType: commandType);
        }


        //public IEnumerable<T> Query<T>(string query, object param = null, CommandType commandType = CommandType.Text)
        //{
        //    return Connection.Query<T>(query, param, _transaction, commandType: commandType);
        //}

        //public IEnumerable<dynamic> Query(string query, object param = null, CommandType commandType = CommandType.Text)
        //{
        //    return Connection.Query(query, param, _transaction, commandType: commandType);
        //}


        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }

                    if (_context.ConnectionTransaction != null)
                    {
                        _context.ConnectionTransaction.Dispose();
                        Trace.WriteLine(">>>>>> [ CONEXÃO TRANSACIONAL SÍNCRONA !- E N C E R R A D A -! ]");
                        Trace.WriteLine(_context.ConnectionTransaction.State, ">>>>>> State: {0}");
                    }
                }

                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
