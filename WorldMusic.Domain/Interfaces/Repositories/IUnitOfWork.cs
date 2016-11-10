using System;
using System.Data;

namespace WorldMusic.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IDbTransaction BeginTransaction(IsolationLevel IsolationLevel = IsolationLevel.ReadCommitted);

        IMusicRepository MusicRepository { get; }

        void Commit();

        void Rollback();
    }
}
