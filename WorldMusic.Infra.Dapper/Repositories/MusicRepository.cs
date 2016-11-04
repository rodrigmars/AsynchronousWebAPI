using WorldMusic.Domain.Entities;
using WorldMusic.Domain.Interfaces.Repositories;
using WorldMusic.Infra.Dapper.Interface;

namespace WorldMusic.Infra.Dapper.Repositories
{
    public class MusicRepository: RepositoryBase<Music>, IMusicRepository
    {
        private readonly IDapperDbContext _context;

        public MusicRepository(IDapperDbContext context): base(context)
        {
            _context = context;
        }
    }
}
