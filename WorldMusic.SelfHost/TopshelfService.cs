using Microsoft.Owin.Hosting;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using System;
using System.Configuration;
using WorldMusic.Api;
using WorldMusic.CrossCutting.IoC;

namespace WorldMusic.SelfHost
{
    public class TopshelfService
    {
        private IDisposable _app;
        const string BASEADDRESS = "http://localhost:9005/";
        string connectionString { get { return ConfigurationManager.ConnectionStrings["TestWorldMusicConnection"].ConnectionString; } }

        public void Start()
        {

            var container = new Container();

            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

            BootStrapper.Register(container, connectionString);


            //_app = WebApp.Start<Startup>(url: BASEADDRESS);
            _app = WebApp.Start(BASEADDRESS, appBuilder =>
             {
                 new Startup().Configuration(appBuilder, container);
             });
        }

        public void Stop()
        {
            _app?.Dispose();
        }
    }
}
