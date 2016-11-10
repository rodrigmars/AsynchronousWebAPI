using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace WorldMusic.Infra.Dapper.Interface
{
    public interface IDapperDbContext
    {
        Task<T> ConnectionAsync<T>(Func<IDbConnection, Task<T>> getData);

        SqlConnection Connection { get; set; }

        void Closed();

        void MonitoringConnection();

    }
}