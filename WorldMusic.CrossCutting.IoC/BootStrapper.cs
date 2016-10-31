using SimpleInjector;
using WorldMusic.Domain.Interfaces.Repositories;
using WorldMusic.Infra.Dapper.DbContext;
using WorldMusic.Infra.Dapper.Interface;
using WorldMusic.Infra.Dapper.Repositories;

namespace WorldMusic.CrossCutting.IoC
{
    public class BootStrapper
    {
        public static void Register(Container container, string connectionString = null)
        {
            //OBJECT CONNECTION
            container.RegisterSingleton<IDapperDbContext>(new DapperDbContext(connectionString));

            //container.RegisterInitializer<DapperDbContext>(handlerToInitialize => {
            //    handlerToInitialize = ;
            //});

            container.Register(typeof(IRepositoryBase<>), typeof(RepositoryBase<>), Lifestyle.Scoped);

            //REPOSITORIES
            //container.Register(typeof(IRepositoryBase<>), typeof(RepositoryBase<>), Lifestyle.Scoped);
        }
    }
}
