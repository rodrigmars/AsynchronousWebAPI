using System;
using System.Data;
using System.Threading.Tasks;

namespace WorldMusic.Infra.Dapper.Interface
{
    public interface IDapperDbContext
    {
        Task<T> ConnectionAsync<T>(Func<IDbConnection, Task<T>> getData);

        IDbConnection Connection { get; }

        void Disposed(bool disposed);
    }
}