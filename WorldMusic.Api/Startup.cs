using Owin;
using System.Web.Http;
using WorldMusic.Api.Jil;
using Microsoft.Owin.Cors;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.WebApi;

namespace WorldMusic.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app, Container container)
        {
            app.Use(async (context, next) =>
            {
                using (var scope = container.BeginExecutionContextScope())
                {
                    context.TraceOutput.WriteLine("CONTAINER INICIADO");

                    await next.Invoke();
                }

                context.TraceOutput.WriteLine("CONTAINER FINALIZADO");
            });

            var config = new HttpConfiguration();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            //Web API routes
            config.MapHttpAttributeRoutes();

            // Remove the JSON formatter
            //config.Formatters.Remove(config.Formatters.JsonFormatter);

            // Remove the XML formatter
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //Jil MediaTypeFormatter
            config.Formatters.RemoveAt(0);
            config.Formatters.Insert(0, new JilFormatter());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Enable CORS for Web API
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
    }
}
