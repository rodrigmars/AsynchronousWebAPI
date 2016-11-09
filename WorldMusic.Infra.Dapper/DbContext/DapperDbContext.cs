using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using WorldMusic.Infra.Dapper.Interface;

namespace WorldMusic.Infra.Dapper.DbContext
{
    public class DapperDbContext : IDapperDbContext
    {
        SqlConnection _connection { get; set; }
        private readonly string _connString;
        bool _disposed { get; set; }

        public DapperDbContext(string connString)
        {
            _connString = connString;
        }

        public async Task<T> ConnectionAsync<T>(Func<IDbConnection, Task<T>> getData)
        {
            using (var connection = new SqlConnection(_connString))
            {
                await connection.OpenAsync();

                TraceDiagnostic(">>>>>> [ CONEXÃO ASSÍNCRONA INICIADA. ]", connection);
                //Trace.WriteLine(">>>>>> [ CONEXÃO ASSÍNCRONA EM EXECUÇÃO ]");
                //Trace.WriteLine(connection.ClientConnectionId, ">>>>>> connection.ClientConnectionId: {0}");
                //Trace.WriteLine(connection.ServerVersion, ">>>>>> ServerVersion: {0}");
                //Trace.WriteLine(connection.State, ">>>>>> State: {0}");

                return await getData(connection);
            }

        }

        public IDbConnection Connection
        {
            get
            {
                if(_connection == null) _connection = new SqlConnection(_connString);

                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();

                    TraceDiagnostic(">>>>>> [ CONEXÃO SÍNCRONA INICIADA. ]", _connection);
                }

                return _connection;
            }
        }

        public void Disposed(bool disposed)
        {
            if (_connection.State == ConnectionState.Open) _connection.Dispose();          
        }

        void TraceDiagnostic(string log, SqlConnection conn)
        {
            Trace.WriteLine(log);
            Trace.WriteLine(conn.ClientConnectionId, ">>>>>> connection.ClientConnectionId: {0}");
            Trace.WriteLine(conn.ServerVersion, ">>>>>> ServerVersion: {0}");
            Trace.WriteLine(conn.State, ">>>>>> State: {0}");
        }
    }
}
