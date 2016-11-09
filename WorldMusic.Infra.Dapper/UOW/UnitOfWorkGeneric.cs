using System;
using System.Data;
using WorldMusic.Domain.Interfaces.Repositories;
using WorldMusic.Infra.Dapper.Interface;
using WorldMusic.Infra.Dapper.Repositories;

namespace WorldMusic.Infra.Dapper.UOW
{
    public class UnitOfWorkGeneric : IUnitOfWorkGeneric
    {
        private readonly IDapperDbContext _context;
        private IDbTransaction _transaction { get; set; }
        private bool _disposed;

        public UnitOfWorkGeneric(IDapperDbContext context)
        {
            _context = context;
        }

        private IMusicRepository _musicRepository { get; set; }
        
        public IMusicRepository MusicRepository
        {
            get
            {
                return _musicRepository ?? (_musicRepository = new MusicRepository(_context));
            }
        }

        public IDbTransaction BeginTransaction(IsolationLevel IsolationLevel = IsolationLevel.ReadCommitted)
        {
            if (_transaction == null) _transaction = _context.Connection.BeginTransaction(IsolationLevel);

            return _transaction;
        }

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

                    //if (_context.Connection != null)
                    //{
                    //    //_connection.Close();
                    //    _context.Connection.Dispose();
                    //}
                }

                _context.Disposed(_disposed = true);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
