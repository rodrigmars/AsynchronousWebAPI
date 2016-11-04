using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using System.Configuration;
using SimpleInjector.Extensions.ExecutionContextScoping;
using WorldMusic.CrossCutting.IoC;
using WorldMusic.Domain.Interfaces.Repositories;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnitTestWorldMusic.MockData;
using SimpleInjector.Integration.WebApi;

namespace UnitTestWorldMusic
{
    [TestClass]
    public class UnitTestUnitOfWork
    {
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

            container.Verify();
        }

        //[Ignore]
        [TestMethod]
        public void Testeinserindoregistrosemparalelo()
        {
            var tasks = new List<Task>();

            foreach (var music in MoqMusic.MoqMusics)
            {
                tasks.Add(Task.Run(() =>
                {
                    //await Task.Delay(new Random().Next(4251, 8550));
                    //await Task.Delay(10);

                    using (container.BeginExecutionContextScope())
                    {
                        var sut = container.GetInstance<IUnitOfWork>();

                        try
                        {
                            sut.BeginTransaction();
                            sut.Execute(@"INSERT INTO [Mercadorias].[dbo].[Musics](
                                Title,
                                Track,
                                IsActive,
                                IDProcess,
                                [TimeStamp]) VALUES(
                                            @Title,
                                            @Track,
                                            @IsActive,
                                            @IDProcess,
                                            GETDATE())", music);

                            //if (!music.IsActive) sut.Commit();
                            sut.Commit();

                        }
                        catch (Exception ex)
                        {
                            sut.Rollback();
                        }
                        finally
                        {
                            sut.Dispose();
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}