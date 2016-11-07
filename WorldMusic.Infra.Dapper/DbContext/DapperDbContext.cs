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
            _connection = new SqlConnection(_connString);
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

        public T Connection_<T>(Func<IDbConnection, T> getData)
        {
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();

                TraceDiagnostic(">>>>>> [ CONNEXÃO SÍNCRONA INICIADA. ]", connection);

                //Trace.WriteLine(">>>>>> [ CONNEXÃO SÍNCRONA INCIADA ]");
                //Trace.WriteLine(connection.ClientConnectionId, ">>>>>> connection.ClientConnectionId: {0}");
                //Trace.WriteLine(connection.ServerVersion, ">>>>>> ServerVersion: {0}");
                //Trace.WriteLine(connection.State, ">>>>>> State: {0}");

                return getData(connection);
            }
        }

        public IDbConnection Connection
        {
            get
            {
                if (_disposed) return null;

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
            _disposed = disposed;
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
