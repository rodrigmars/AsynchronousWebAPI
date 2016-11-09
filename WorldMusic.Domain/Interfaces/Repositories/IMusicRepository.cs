using System;
using System.Collections.Generic;
using System.Data;
using WorldMusic.Domain.Entities;

namespace WorldMusic.Domain.Interfaces.Repositories
{
    public interface IMusicRepository : IRepositoryBase<Music>
    {
        IEnumerable<Music> GetAllInactiveTracks(string query, object param = null, CommandType command = CommandType.Text);

        bool AddMusicMusicByStyle(string query, object param = null, CommandType command = CommandType.Text);

        bool Add(Music music, IDbTransaction tran = null, CommandType command = CommandType.Text);

        bool RemoveInactiveMusic(object param = null, IDbTransaction tran = null, CommandType command = CommandType.Text);

        void Dispose();
    }
}