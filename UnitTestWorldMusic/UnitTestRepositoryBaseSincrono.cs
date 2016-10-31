using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using WorldMusic.CrossCutting.IoC;
using System.Configuration;
using SimpleInjector.Extensions.ExecutionContextScoping;
using WorldMusic.Domain.Interfaces.Repositories;
using WorldMusic.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTestWorldMusic
{

    //USE master
    //DROP TABLE Musics

    //CREATE TABLE Musics(
    //MusicId int IDENTITY(1,1) NOT NULL,
    //Title varchar(50) NULL,
    //Track int NOT NULL,
    //IsActive bit NOT NULL,
    //IDProcess int NOT NULL,
    //[TimeStamp]
    //datetime NULL
    //)
    //ALTER TABLE Musics ADD CONSTRAINT PKMusics PRIMARY KEY(MusicId)
        
    //select* from Musics where IDProcess = 107

    //select session_id, program_name
    //from sys.dm_exec_sessions
    //where program_name = "ConnPoolWorldMusic";

    //   select* FROM sys.dm_exec_sessions AS es
    //INNER JOIN sys.dm_exec_connections AS ec
    //ON es.session_id = ec.session_id

    [TestClass]
    public class UnitTestRepositoryBaseSincrono
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
        public void TestGetAllMusics()
        {
            using (container.BeginExecutionContextScope())
            {
                // Arrrange
                var sut = container.GetInstance<IRepositoryBase<Discography>>();

                // Act
                var result = sut.GetAll("SELECT * FROM [Mercadorias].[dbo].[Musics]");

                // Assert
                Assert.IsTrue(result.ToList().Count() > 1);
            }
        }

        [Ignore]
        //[TestMethod]
        public void TestDeleteMusicsById()
        {
            using (container.BeginExecutionContextScope())
            {
                // Arrrange
                var sut = container.GetInstance<IRepositoryBase<Discography>>();

                var query = @"DELETE FROM [Mercadorias].[dbo].[Musics] WHERE [Mercadorias].[dbo].[Musics] = @MusicID";

                // Act
                var rowsAffected = sut.Remove(query, new { MusicID = 2512 });

                // Assert
                Assert.IsTrue(rowsAffected);
            }
        }

        //[Ignore]
        [TestMethod]
        public void Testeutilizandochamadasassíncronasparainclusãoderegistros()
        {
            using (container.BeginExecutionContextScope())
            {
                var task1 = Task.Run(async () =>
                {
                    await Task.Delay(1520);

                    var sut = container.GetInstance<IRepositoryBase<Discography>>();

                    var music = new Music
                    {
                        Title = "Sithu Aye - 4521",
                        Track = 5,
                        IsActive = false,
                        IDProcess = 4521,
                    };

                    var query = @"INSERT INTO [Mercadorias].[dbo].[Musics](Title,
                                Track,
                                IsActive,
                                IDProcess,
                                [TimeStamp]) VALUES(@Title, @Track, @IsActive, @IDProcess, GETDATE())";

                    var rowsAffected = sut.Add(query,
                        new
                        {
                            @Title = music.Title,
                            @Track = music.Track,
                            @IsActive = music.IsActive,
                            @IDProcess = music.IDProcess
                        });

                    // Assert
                    Assert.IsTrue(rowsAffected);
                });

                var task2 = Task.Run(async () =>
                {
                    await Task.Delay(3500);

                    var sut = container.GetInstance<IRepositoryBase<Discography>>();

                    var music = new Music
                    {
                        Title = "Gru - 550",
                        Track = 3,
                        IsActive = false,
                        IDProcess = 550
                    };

                    var query = @"INSERT INTO [Mercadorias].[dbo].[Musics](Title,
                                Track,
                                IsActive,
                                IDProcess,
                                [TimeStamp]) VALUES(@Title, @Track, @IsActive, @IDProcess, GETDATE())";

                    var rowsAffected = sut.Add(query,
                        new
                        {
                            @Title = music.Title,
                            @Track = music.Track,
                            @IsActive = music.IsActive,
                            @IDProcess = music.IDProcess
                        });

                    // Assert
                    Assert.IsTrue(rowsAffected);
                });

                var task3 = Task.Run(async () =>
                {
                    await Task.Delay(9000);

                    var sut = container.GetInstance<IRepositoryBase<Discography>>();

                    var music = new Music
                    {
                        Title = "Mestis - Menta - 3500",
                        Track = 5,
                        IsActive = true,
                        IDProcess = 3500
                    };

                    var query = @"INSERT INTO [Mercadorias].[dbo].[Musics](Title,
                                Track,
                                IsActive,
                                IDProcess,
                                [TimeStamp]) VALUES(@Title, @Track, @IsActive, @IDProcess, GETDATE())";

                    var rowsAffected = sut.Add(query,
                        new
                        {
                            @Title = music.Title,
                            @Track = music.Track,
                            @IsActive = music.IsActive,
                            @IDProcess = music.IDProcess
                        });

                    // Assert
                    Assert.IsTrue(rowsAffected);
                });

                Task.WhenAll(task1, task2, task3).Wait();
            }
        }

        [Ignore]
        //[TestMethod]
        public void Testeutilizandochamadasassíncronasparaexclusãoderegistros()
        {
            using (container.BeginExecutionContextScope())
            {
                var task1 = Task.Run(() =>
                {

                    var sut = container.GetInstance<IRepositoryBase<Discography>>();

                    var query = @"DELETE FROM [Mercadorias].[dbo].[Musics] WHERE MusicID = @MusicID";

                    var rowsAffected = sut.Remove(query, new { MusicID = 4 });

                    // Assert
                    Assert.IsTrue(rowsAffected);
                });

                var task2 = Task.Run(() =>
                {

                    var sut = container.GetInstance<IRepositoryBase<Discography>>();

                    var query = @"DELETE FROM [Mercadorias].[dbo].[Musics] WHERE MusicID = @MusicID";

                    // Act
                    var rowsAffected = sut.Remove(query, new { MusicID = 6 });

                    // Assert
                    Assert.IsTrue(rowsAffected);
                });

                var task3 = Task.Run(() =>
                {

                    var sut = container.GetInstance<IRepositoryBase<Discography>>();

                    var query = @"DELETE FROM [Mercadorias].[dbo].[Musics] WHERE MusicID = @MusicID";

                    // Act
                    var rowsAffected = sut.Remove(query, new { MusicID = 8 });

                    // Assert
                    Assert.IsTrue(rowsAffected);
                });

                Task.WhenAll(task1, task2, task3).Wait();
            }
        }
    }
}
