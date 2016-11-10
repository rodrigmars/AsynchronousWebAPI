using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using WorldMusic.Infra.Dapper.Interface;

namespace WorldMusic.Infra.Dapper.DbContext
{
    public class DapperDbContext : IDisposable, IDapperDbContext
    {
        private SqlConnection _connection;

        private readonly string _connString;

        bool disposed;

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

                return await getData(connection);
            }
        }

        public SqlConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(_connString);

                    OpenConnection();

                    TraceDiagnostic(">>>>>> [**** INSTÂNCIA CRIADA E CONEXÃO ABERTA PARA SQL CONNECTION ****]", _connection);
                }

                if (_connection.State == ConnectionState.Closed)
                {
                    OpenConnection();

                    TraceDiagnostic(">>>>>> [ NOVA CONEXÃO ABERTA ]", _connection);
                }

                return _connection;
            }
            set
            {
                _connection = value;
            }
        }


        void OpenConnection()
        {
            _connection.Open();
        }

        public void Closed()
        {
            if (_connection.State == ConnectionState.Open) _connection.Close();
        }

        /// <summary>
        /// Monitora status CONNECTION
        /// </summary>
        public void MonitoringConnection()
        {
            TraceDiagnostic(">>>>>> MONITORANDO CONEXAO", _connection);
        }

        void TraceDiagnostic(string log, SqlConnection conn)
        {
            Trace.WriteLine(log);
            Trace.WriteLine(conn.ClientConnectionId, ">>>>>> connection.ClientConnectionId: {0}");

            if (conn.State == ConnectionState.Open) Trace.WriteLine(conn.ServerVersion, ">>>>>> ServerVersion: {0}");

            Trace.WriteLine(conn.State, ">>>>>> State: {0}");
        }

        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _connection.Dispose();

                    TraceDiagnostic(">>>>>> DISPOSE REALIZADO PARA SQL CONNECTION ", _connection);
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DapperDbContext() // the finalizer
        {
            Dispose(false);
        }

    }
}
