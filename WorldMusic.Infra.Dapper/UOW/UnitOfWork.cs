using System;
using System.Data;
using WorldMusic.Domain.Interfaces.Repositories;
using WorldMusic.Infra.Dapper.Interface;
using WorldMusic.Infra.Dapper.Repositories;

namespace WorldMusic.Infra.Dapper.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly IDapperDbContext _context;
        IDbTransaction _transaction { get; set; }
        IMusicRepository _musicRepository { get; set; }

        public UnitOfWork(IDapperDbContext context)
        {
            _context = context;
        }

        public IMusicRepository MusicRepository
        {
            get
            {
                return _musicRepository ?? (_musicRepository = new MusicRepository(_context));
            }
        }

        public IDbTransaction BeginTransaction(IsolationLevel IsolationLevel = IsolationLevel.ReadCommitted)
        {
            return _transaction ?? (_transaction = _context.Connection.BeginTransaction(IsolationLevel));
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
                //_transaction = null;
            }
        }

        bool _disposed { get; set; }

        protected virtual void Dispose(bool disposing)
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
                }
                //_context.Disposed(_disposed = true);
                //_context.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
