using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using WorldMusic.Domain.Entities;
using System.Threading;
using System.Diagnostics;
using System.Linq;
using WorldMusic.Api.V1.Controller;
using System.Web.Http.Results;
using SimpleInjector;
using System.Configuration;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Extensions.ExecutionContextScoping;
using WorldMusic.Domain.Interfaces.Repositories;
using WorldMusic.CrossCutting.IoC;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UnitTestWorldMusic.INTEGRADO
{
    [TestClass]
    public class UnitTestCrud
    {
        //public Process proc { get; set; }

        //[TestInitialize]
        //public void Setup()
        //{

        //    proc = new Process
        //    {
        //        StartInfo = new ProcessStartInfo
        //        {
        //            FileName = @"F:\GIT\WorldMusic\WorldMusic.SelfHost\bin\Debug\WorldMusic.SelfHost.exe",
        //            Arguments = "run",
        //            UseShellExecute = false,
        //            RedirectStandardOutput = true,
        //            CreateNoWindow = true,

        //        }
        //    };

        //    proc.Start();

        //    //Debug.WriteLine(proc.Id);

        //    //while (!proc.StandardOutput.EndOfStream)
        //    //{
        //    //    string line = proc.StandardOutput.ReadLine();
        //    //    // do something with line
        //    //}
        //}

        //[TestCleanup]
        //public void KillProcess()
        //{
        //    var processDebug = Process.GetProcesses().Where(pr => pr.ProcessName == proc.ProcessName);

        //    foreach (var process in processDebug) process.Kill();

        //    proc.Dispose();
        //}


        //[TestMethod]
        //public void Testintegradocadastrodemusica()
        //{

        //    var client = new HttpClient();

        //    client.BaseAddress = new Uri("http://localhost:9005/");
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    var response = client.GetAsync("api/v1/musics").Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var musics = response.Content.ReadAsAsync<IEnumerable<Music>>().Result;

        //        foreach (var music in musics)
        //        {
        //            //Call your store method and pass in your own object
        //            //SaveCustomObjectToDB(x);
        //        }
        //    }
        //    else
        //    {
        //        //Something has gone wrong, handle it here
        //    }
        //}

        Container container { get; set; }
        string connectionString { get { return ConfigurationManager.ConnectionStrings["TestWorldMusicConnection"].ConnectionString; } }

        [TestInitialize]
        public void Setup()
        {

            container = new Container();
            //container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
            //container.Options.DefaultScopedLifestyle = new LifetimeScopeLifestyle();

            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            BootStrapper.Register(container, connectionString);

            //container.Verify();


        }

        [TestMethod]
        public void Testinjetandoconexõesparalelas()
        {
            //var mockRepository = new Mock<IProductRepository>();
            //mockRepository.Setup(x => x.GetById(42))
            //    .Returns(new Product { Id = 42 });

            var tasks = new List<Task>();

            var numRequisitions = 100000;

            for (int i = 0; i < numRequisitions; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    using (container.BeginExecutionContextScope())
                    {
                        using (var _connection = new SqlConnection(connectionString))
                        {
                            _connection.Open();
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
        }

        [TestMethod]
        public void Test()
        {

            //var mockRepository = new Mock<IProductRepository>();
            //mockRepository.Setup(x => x.GetById(42))
            //    .Returns(new Product { Id = 42 });

            var tasks = new List<Task>();

            var numRequisitions = 20000;

            for (int i = 0; i < numRequisitions; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    using (container.BeginExecutionContextScope())
                    {
                        var repoUOW = container.GetInstance<IUnitOfWorkGeneric>();
                        var repoMusic = container.GetInstance<IMusicRepository>();


                        var controller = new MusicsController(repoUOW, repoMusic);

                        // Act
                        var actionResult = controller.GetAll();

                        var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Music>>;

                        Assert.IsTrue(contentResult.Content.Count() > 0);
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
        }

        [TestMethod]
        public void TestRemoveTrackById()
        {
            var tasks = new List<Task>();

            var numRequisitions = 1;

            for (int i = 0; i < numRequisitions; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    using (container.BeginExecutionContextScope())
                    {
                        var repoUOW = container.GetInstance<IUnitOfWorkGeneric>();
                        var repoMusic = container.GetInstance<IMusicRepository>();

                        var controller = new MusicsController(repoUOW, repoMusic);

                        var actionResult = controller.RemoveTrackById(1, true);

                        var contentResult = actionResult as OkNegotiatedContentResult<bool>;

                        Assert.IsTrue(contentResult.Content);
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}
