using System;
using System.Collections.Generic;
using System.Data;

namespace WorldMusic.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();

        void Commit();
        void Rollback();

        int Execute(string query, object param = null, CommandType commandType = CommandType.Text);

        //IEnumerable<T> Query<T>(string query, object param = null, CommandType commandType = CommandType.Text);

        //IEnumerable<dynamic> Query(string query, object param = null, CommandType commandType = CommandType.Text);
    }
}
