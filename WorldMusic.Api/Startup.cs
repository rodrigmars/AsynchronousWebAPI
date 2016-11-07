using Owin;
using System.Web.Http;
using WorldMusic.Api.Jil;
using Microsoft.Owin.Cors;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.WebApi;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin;
using System;
using WorldMusic.Api.OAuth;

namespace WorldMusic.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app, Container container)
        {

            var config = new HttpConfiguration();

            ConfigureDI(app, container, config);

            ConfigureRoutes(config);

            ConfigureOAuth(app);

            ConfigureJil(config);

            // Enable CORS for Web API
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        void ConfigureRoutes(HttpConfiguration config)
        {
            //Web API routes
            config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        void ConfigureDI(IAppBuilder app, Container container, HttpConfiguration config)
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

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

        }

        void ConfigureJil(HttpConfiguration config)
        {
            // Remove the JSON formatter
            //config.Formatters.Remove(config.Formatters.JsonFormatter);

            // Remove the XML formatter
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //Jil MediaTypeFormatter
            config.Formatters.RemoveAt(0);
            config.Formatters.Insert(0, new JilFormatter());
        }

        void ConfigureOAuth(IAppBuilder app)
        {
            var OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //Acesso sem https habilitada;
                AllowInsecureHttp = true,
                
                //
                TokenEndpointPath = new PathString("/api/secutiry/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(2),
                
                //ProviderDeloitte
                Provider = new AuthorizationServerProviderDeloitte()
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

    }
}