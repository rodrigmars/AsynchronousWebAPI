﻿using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WorldMusic.Domain.Entities;
using WorldMusic.Domain.Interfaces.Repositories;
using WorldMusic.Infra.Dapper.Interface;

namespace WorldMusic.Infra.Dapper.Repositories
{
    public class MusicRepository : RepositoryBase<Music>, IMusicRepository
    {

        public MusicRepository(IDapperDbContext context) : base(context) { }

        public IEnumerable<Music> GetAllInactiveTracks(string query, object param = null, CommandType command = CommandType.Text)
        {
            return _context.Connection.Query<Music>(query, param, commandType: command);
        }

        public bool AddMusicMusicByStyle(string query, object param = null, CommandType command = CommandType.Text)
        {
            return _context.Connection.Execute(query, param, commandType: command) > 0;
        }

        public bool RemoveInactiveMusic(object param = null, IDbTransaction tran = null, CommandType command = CommandType.Text)
        {

            var query = @"SELECT * FROM MUSICS WHERE 
                        MUSICID = @MusicId 
                        AND ISACTIVE = @IsActive";
            var music = _context.Connection.Query<Music>(query, param, transaction: tran, commandType: command);

            if (music.Count() == 0) return false;

            query = @"DELETE MUSICS WHERE 
                        MUSICID = @MusicId 
                        AND ISACTIVE = @IsActive";

            return _context.Connection.Execute(query, music, transaction: tran, commandType: command) > 0;
        }

        public bool Add(Music music, IDbTransaction tran = null, CommandType command = CommandType.Text) {

            var query = @"INSERT INTO MUSICS VALUES(@Title
                                   ,@Track
                                   ,@IsActive
                                   ,@IDProcess
                                   ,GETDATE())";

            return _context.Connection.Execute(query, music, transaction: tran, commandType: command) > 0;
        }

        public bool UpdateMusicByBandOfMetal(Music music, CommandType command = CommandType.Text)
        {
            var query = @"UPDATE MUSICS SET TITLE = @Name, 
                        IDProcess = @IdProcess WHERE MusicId = @Id";

            return _context.Connection.Execute(query, new
            {
                Name = music.Title,
                IdProcess = music.IDProcess,
                Id = music.MusicId
            }, commandType: command) > 0;
        }
    }
}
