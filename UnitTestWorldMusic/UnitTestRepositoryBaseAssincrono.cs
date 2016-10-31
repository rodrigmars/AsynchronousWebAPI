using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using WorldMusic.CrossCutting.IoC;
using WorldMusic.Domain.Interfaces.Repositories;
using WorldMusic.Domain.Entities;
using System.Configuration;
using SimpleInjector.Extensions.ExecutionContextScoping;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTestWorldMusic
{
    [TestClass]
    public class UnitTestRepositoryBaseAssincrono
    {
        Container container { get; set; }
        string connectionString { get { return ConfigurationManager.ConnectionStrings["TestWorldMusicConnection"].ConnectionString; } }

        [TestInitialize]
        public void Setup()
        {
            container = new Container();
            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
            BootStrapper.Register(container, connectionString);

            container.Verify();
        }

        [Ignore]
        //[TestMethod]
        public void TestInsere()
        {
            for (int i = 0; i < 10; i++)
            {

                var task01 = Task.Run(async () =>
                {
                    await Task.Delay(2);

                    using (container.BeginExecutionContextScope())
                    {
                        var sut = container.GetInstance<IRepositoryBase<Discography>>();

                        var music = new Music
                        {
                            Title = "So It Is Below",
                            Track = 4,
                            IsActive = true,
                            IDProcess = 105
                        };

                        var result = await sut.AddAsync(
                            "INSERT INTO DBO.Musics(Title, Track, IsActive, IDProcess) " +
                            "VALUES(@Title, @Track, @IsActive, @IDProcess)",
                            new { @Title = music.Title, @Track = music.Track, @IsActive = music.IsActive, @IDProcess = music.IDProcess });

                        Assert.IsTrue(result);
                    }


                });

                var task02 = Task.Run(async () =>
                {
                //await Task.Delay(2);

                using (container.BeginExecutionContextScope())
                    {
                        var sut = container.GetInstance<IRepositoryBase<Discography>>();

                        var music = new Music
                        {
                            Title = "Radiant",
                            Track = 9,
                            IsActive = true,
                            IDProcess = 106
                        };

                        var result = await sut.AddAsync(
                            "INSERT INTO DBO.Musics(Title, Track, IsActive, IDProcess) " +
                            "VALUES(@Title, @Track, @IsActive, @IDProcess)",
                            new { @Title = music.Title, @Track = music.Track, @IsActive = music.IsActive, @IDProcess = music.IDProcess });

                        Assert.IsTrue(result);
                    }
                });

                var task03 = Task.Run(async () =>
                {
                //await Task.Delay(2);

                using (container.BeginExecutionContextScope())
                    {
                        var sut = container.GetInstance<IRepositoryBase<Discography>>();

                        var music = new Music
                        {
                            Title = "Layers",
                            Track = 14,
                            IsActive = true,
                            IDProcess = 107
                        };

                        var result = await sut.AddAsync(
                            "INSERT INTO DBO.Musics(Title, Track, IsActive, IDProcess) " +
                            "VALUES(@Title, @Track, @IsActive, @IDProcess)",
                            new { @Title = music.Title, @Track = music.Track, @IsActive = music.IsActive, @IDProcess = music.IDProcess });

                        Assert.IsTrue(result);
                    }


                });

                Task.WhenAll(task01, task02, task03).Wait();

            }
        }

        [Ignore]
        //[TestMethod]
        public void TestAdd()
        {

            for (int i = 0; i < 1000; i++)
            {
                var teste01 = Task.Run(async () =>
            {
                await Task.Delay(2);

                //Thread.Sleep();

                //container = new Container();
                //container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
                //BootStrapper.Register(container, connectionString);


                using (container.BeginExecutionContextScope())
                {
                    var sut = container.GetInstance<IRepositoryBase<Discography>>();

                    var result = await sut.GetAllAsync("SELECT * FROM DBO.DISCOGRAPHY");

                    Assert.IsTrue(result.ToList().Count() > 0);
                }


            });
                var teste02 = Task.Run(async () =>
                {
                    await Task.Delay(2);

                    //Thread.Sleep();

                    //container = new Container();
                    //container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
                    //BootStrapper.Register(container, connectionString);


                    using (container.BeginExecutionContextScope())
                    {
                        var sut = container.GetInstance<IRepositoryBase<Discography>>();

                        var result = await sut.GetAllAsync("SELECT * FROM DBO.DISCOGRAPHY");

                        Assert.IsTrue(result.ToList().Count() > 0);
                    }


                });
                var teste03 = Task.Run(async () =>
                {
                    await Task.Delay(2);

                    //Thread.Sleep();

                    //container = new Container();
                    //container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
                    //BootStrapper.Register(container, connectionString);


                    using (container.BeginExecutionContextScope())
                    {
                        var sut = container.GetInstance<IRepositoryBase<Discography>>();

                        var result = await sut.GetAllAsync("SELECT * FROM DBO.DISCOGRAPHY");

                        Assert.IsTrue(result.ToList().Count() > 0);
                    }


                });
                var teste04 = Task.Run(async () =>
                {
                    await Task.Delay(2);

                    //Thread.Sleep();

                    //container = new Container();
                    //container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
                    //BootStrapper.Register(container, connectionString);


                    using (container.BeginExecutionContextScope())
                    {
                        var sut = container.GetInstance<IRepositoryBase<Discography>>();

                        var result = await sut.GetAllAsync("SELECT * FROM DBO.DISCOGRAPHY");

                        Assert.IsTrue(result.ToList().Count() > 0);
                    }


                });


                var teste05 = Task.Run(async () =>
                {
                    await Task.Delay(2);

                    //Thread.Sleep();

                    //container = new Container();
                    //container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
                    //BootStrapper.Register(container, connectionString);


                    using (container.BeginExecutionContextScope())
                    {
                        var sut = container.GetInstance<IRepositoryBase<Discography>>();

                        var result = await sut.GetAllAsync("SELECT * FROM DBO.DISCOGRAPHY");

                        Assert.IsTrue(result.ToList().Count() > 0);
                    }


                });
                var teste06 = Task.Run(async () =>
                {
                    await Task.Delay(2);

                    //Thread.Sleep();

                    //container = new Container();
                    //container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
                    //BootStrapper.Register(container, connectionString);


                    using (container.BeginExecutionContextScope())
                    {
                        var sut = container.GetInstance<IRepositoryBase<Discography>>();

                        var result = await sut.GetAllAsync("SELECT * FROM DBO.DISCOGRAPHY");

                        Assert.IsTrue(result.ToList().Count() > 0);
                    }


                });
                var teste07 = Task.Run(async () =>
                {
                    await Task.Delay(2);

                    //Thread.Sleep();

                    //container = new Container();
                    //container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
                    //BootStrapper.Register(container, connectionString);


                    using (container.BeginExecutionContextScope())
                    {
                        var sut = container.GetInstance<IRepositoryBase<Discography>>();

                        var result = await sut.GetAllAsync("SELECT * FROM DBO.DISCOGRAPHY");

                        Assert.IsTrue(result.ToList().Count() > 0);
                    }


                });
                var teste08 = Task.Run(async () =>
                {
                    await Task.Delay(2);

                    //Thread.Sleep();

                    //container = new Container();
                    //container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
                    //BootStrapper.Register(container, connectionString);


                    using (container.BeginExecutionContextScope())
                    {
                        var sut = container.GetInstance<IRepositoryBase<Discography>>();

                        var result = await sut.GetAllAsync("SELECT * FROM DBO.DISCOGRAPHY");

                        Assert.IsTrue(result.ToList().Count() > 0);
                    }

                });


                var teste09 = Task.Run(async () =>
                {
                    await Task.Delay(2);

                    //Thread.Sleep();

                    //container = new Container();
                    //container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
                    //BootStrapper.Register(container, connectionString);


                    using (container.BeginExecutionContextScope())
                    {
                        var sut = container.GetInstance<IRepositoryBase<Discography>>();

                        var result = await sut.GetAllAsync("SELECT * FROM DBO.DISCOGRAPHY");

                        Assert.IsTrue(result.ToList().Count() > 0);
                    }

                });

                var teste10 = Task.Run(async () =>
                {
                    await Task.Delay(2);

                    //Thread.Sleep();

                    //container = new Container();
                    //container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
                    //BootStrapper.Register(container, connectionString);


                    using (container.BeginExecutionContextScope())
                    {
                        var sut = container.GetInstance<IRepositoryBase<Discography>>();

                        var result = await sut.GetAllAsync("SELECT * FROM DBO.DISCOGRAPHY");

                        Assert.IsTrue(result.ToList().Count() > 0);
                    }

                });


                var teste11 = Task.Run(async () =>
                {
                    await Task.Delay(2);

                    //Thread.Sleep();

                    //container = new Container();
                    //container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
                    //BootStrapper.Register(container, connectionString);


                    using (container.BeginExecutionContextScope())
                    {
                        var sut = container.GetInstance<IRepositoryBase<Discography>>();

                        var result = await sut.GetAllAsync("SELECT * FROM DBO.DISCOGRAPHY");

                        Assert.IsTrue(result.ToList().Count() > 0);
                    }

                });


                var teste12 = Task.Run(async () =>
                {
                    await Task.Delay(2);

                    //Thread.Sleep();

                    //container = new Container();
                    //container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
                    //BootStrapper.Register(container, connectionString);


                    using (container.BeginExecutionContextScope())
                    {
                        var sut = container.GetInstance<IRepositoryBase<Discography>>();

                        var result = await sut.GetAllAsync("SELECT * FROM DBO.DISCOGRAPHY");

                        Assert.IsTrue(result.ToList().Count() > 0);
                    }

                });

                var teste13 = Task.Run(async () =>
                {
                    await Task.Delay(2);

                    //Thread.Sleep();

                    //container = new Container();
                    //container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
                    //BootStrapper.Register(container, connectionString);


                    using (container.BeginExecutionContextScope())
                    {
                        var sut = container.GetInstance<IRepositoryBase<Discography>>();

                        var result = await sut.GetAllAsync("SELECT * FROM DBO.DISCOGRAPHY");

                        Assert.IsTrue(result.ToList().Count() > 0);
                    }

                });

                var teste14 = Task.Run(async () =>
                {
                    await Task.Delay(2);

                    //Thread.Sleep();

                    //container = new Container();
                    //container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
                    //BootStrapper.Register(container, connectionString);


                    using (container.BeginExecutionContextScope())
                    {
                        var sut = container.GetInstance<IRepositoryBase<Discography>>();

                        var result = await sut.GetAllAsync("SELECT * FROM DBO.DISCOGRAPHY");

                        Assert.IsTrue(result.ToList().Count() > 0);
                    }

                });

                var teste15 = Task.Run(async () =>
                {
                    await Task.Delay(2);

                    //Thread.Sleep();

                    //container = new Container();
                    //container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
                    //BootStrapper.Register(container, connectionString);


                    using (container.BeginExecutionContextScope())
                    {
                        var sut = container.GetInstance<IRepositoryBase<Discography>>();

                        var result = await sut.GetAllAsync("SELECT * FROM DBO.DISCOGRAPHY");

                        Assert.IsTrue(result.ToList().Count() > 0);
                    }

                });
                Task.WhenAll(teste01,
                    teste02,
                    teste03,
                    teste04,
                    teste05,
                    teste06,
                    teste07,
                    teste08,
                    teste09,
                    teste10,
                    teste11,
                    teste12,
                    teste13,
                    teste14,
                    teste15).Wait();
            }
        }


    }
}
