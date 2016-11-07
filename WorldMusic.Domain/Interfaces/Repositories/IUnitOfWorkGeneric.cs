using System;
using System.Data;

namespace WorldMusic.Domain.Interfaces.Repositories
{
    public interface IUnitOfWorkGeneric : IDisposable
    {
        IDbTransaction BeginTransaction(IsolationLevel IsolationLevel = IsolationLevel.ReadCommitted);

        IMusicRepository MusicRepository { get; }
    }
}
