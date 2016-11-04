using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace WorldMusic.SelfHost
{
    class Program
    {
        static int Main(string[] args)
        {
            var exitCode = HostFactory.Run(config =>
            {

                // Pass it to Topshelf
                //x.UseSimpleInjector(_container);

                config.Service<TopshelfService>(c =>
                {
                    c.ConstructUsing(name => new TopshelfService());
                    //s.ConstructUsingSimpleInjector();
                    c.WhenStarted(tc => tc.Start());
                    c.WhenStopped(tc => tc.Stop());
                });

                config.EnablePauseAndContinue();
                config.EnableShutdown();

                config.StartAutomaticallyDelayed();
                config.RunAsLocalSystem();

                config.SetDescription("Service adm to store music api rest.");
                config.SetDisplayName("Service Adm Store Music API");
                config.SetServiceName("ServiceWorldMusicSelfHost");
            });

            return (int)exitCode;
        }
    }
}
