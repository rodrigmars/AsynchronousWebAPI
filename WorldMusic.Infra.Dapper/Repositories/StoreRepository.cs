using WorldMusic.Domain.Entities;
using WorldMusic.Domain.Interfaces.Repositories;
using WorldMusic.Infra.Dapper.Interface;

namespace WorldMusic.Infra.Dapper.Repositories
{
    public class StoreRepository : RepositoryBase<Store>, IStoreRepository
    {
        public StoreRepository(IDapperDbContext context) : base(context) { }
    }
}
