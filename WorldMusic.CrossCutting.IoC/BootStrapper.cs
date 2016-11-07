using SimpleInjector;
using WorldMusic.Domain.Interfaces.Repositories;
using WorldMusic.Infra.Dapper.DbContext;
using WorldMusic.Infra.Dapper.Interface;
using WorldMusic.Infra.Dapper.Repositories;
using WorldMusic.Infra.Dapper.UOW;

namespace WorldMusic.CrossCutting.IoC
{
    public class BootStrapper
    {
        public static void Register(Container container, string connectionString = null)
        {
            //OBJECT CONNECTION
            container.Register<IDapperDbContext>(() => new DapperDbContext(connectionString), Lifestyle.Scoped);

            //container.RegisterInitializer<DapperDbContext>(handlerToInitialize => {
            //    handlerToInitialize = ;
            //});

            container.Register(typeof(IRepositoryBase<>), typeof(RepositoryBase<>), Lifestyle.Scoped);

            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);

            container.Register<IMusicRepository, MusicRepository>(Lifestyle.Scoped);

            container.Register<IUnitOfWorkGeneric, UnitOfWorkGeneric>(Lifestyle.Scoped);

            

            //container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);

            //REPOSITORIES
            //container.Register(typeof(IRepositoryBase<>), typeof(RepositoryBase<>), Lifestyle.Scoped);
        }
    }
}
