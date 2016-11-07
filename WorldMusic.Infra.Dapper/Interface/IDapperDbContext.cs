using System;
using System.Data;
using System.Threading.Tasks;

namespace WorldMusic.Infra.Dapper.Interface
{
    public interface IDapperDbContext
    {
        Task<T> ConnectionAsync<T>(Func<IDbConnection, Task<T>> getData);

        T Connection_<T>(Func<IDbConnection, T> getData);

        IDbConnection Connection { get; }

        void Disposed(bool disposed);
    }
}