using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using WorldMusic.CrossCutting.IoC;
using System.Configuration;
using SimpleInjector.Extensions.ExecutionContextScoping;
using WorldMusic.Domain.Interfaces.Repositories;
using WorldMusic.Domain.Entities;
using System.Linq;

namespace UnitTestWorldMusic
{

    //    DROP TABLE Musics

    // CREATE TABLE Musics(
    //    MusicId int IDENTITY(1,1) NOT NULL,
    //    Title varchar(50) NULL,
    //	Track int NOT NULL,
    //	IsActive bit NOT NULL,
    //    IDProcess int NOT NULL
    // )
    // ALTER TABLE Musics ADD CONSTRAINT PKMusics PRIMARY KEY(MusicId)
    // --ALTER TABLE Musics ADD CONSTRAINT UQMusicsIDProcess UNIQUE(IDProcess)

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

        //[Ignore]
        [TestMethod]
        public void TestGetAllMusics()
        {
            using (container.BeginExecutionContextScope())
            {
                // Arrrange
                var sut = container.GetInstance<IRepositoryBase<Discography>>();

                // Act
                var result = sut.GetAll("SELECT * FROM DBO.Musics");

                // Assert
                Assert.IsTrue(result.ToList().Count() > 1);
            }
        }


        //[Ignore]
        [TestMethod]
        public void TestDeleteMusicsById()
        {
            using (container.BeginExecutionContextScope())
            {
                // Arrrange
                var sut = container.GetInstance<IRepositoryBase<Discography>>();

                var query = @"DELETE FROM DBO.MUSIC WHERE MusicID = @MusicID";

                // Act
                var rowsAffected = sut.Remove(query, new { MusicID = 2512 });

                // Assert
                Assert.IsTrue(rowsAffected);
            }
        }

        


    }
}
