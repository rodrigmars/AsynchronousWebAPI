using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using WorldMusic.Infra.Dapper.Interface;

namespace WorldMusic.Infra.Dapper.DbContext
{
    public class DapperDbContext: IDapperDbContext
    {
        SqlConnection _dbConnection { get; set; }

        private readonly string _connection;

        public DapperDbContext(string connection)
        {
            _connection = connection;
        }

        public async Task<T> ConnectionAsync<T>(Func<IDbConnection, Task<T>> getData)
        {
            using (var connection = new SqlConnection(_connection))
            {
                await connection.OpenAsync();

                Trace.WriteLine(">>>>>> [ CONEXÃO ASSÍNCRONA EM EXECUÇÃO ]");
                Trace.WriteLine(connection.ClientConnectionId, ">>>>>> connection.ClientConnectionId: {0}");
                Trace.WriteLine(connection.ServerVersion, ">>>>>> ServerVersion: {0}");
                Trace.WriteLine(connection.State, ">>>>>> State: {0}");

                return await getData(connection);
            }
        }

        public T Connection<T>(Func<IDbConnection, T> getData)
        {
            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();

                Trace.WriteLine(">>>>>> [ CONNEXÃO SÍNCRONA EM EXECUÇÃO ]");
                Trace.WriteLine(connection.ClientConnectionId, ">>>>>> connection.ClientConnectionId: {0}");
                Trace.WriteLine(connection.ServerVersion, ">>>>>> ServerVersion: {0}");
                Trace.WriteLine(connection.State, ">>>>>> State: {0}");

                return getData(connection);
            }
        }
    }
}
