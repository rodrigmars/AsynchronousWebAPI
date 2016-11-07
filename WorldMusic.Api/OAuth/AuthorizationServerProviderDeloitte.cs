using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace WorldMusic.Api.OAuth
{
    public class AuthorizationServerProviderDeloitte : OAuthAuthorizationServerProvider
    {
        
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //VALIDA TOKEN NO CACHE?? verifica se token é válido
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            try
            {
                var user = context.UserName;
                var password = context.Password;

                //TODO: IMPLEMENTAR REPOSITÓRIO DE AUTENTICAÇÃO IDENTITY
                if (user != "rodrigo" || password != "teste123")
                {
                    context.SetError("Acesso inválido", "Usário ou senha inválidos");
                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                identity.AddClaim(new Claim(ClaimTypes.Name, user));

                var roles = new List<string>();

                roles.Add("User");

                foreach (var role in roles) identity.AddClaim(new Claim(ClaimTypes.Role, role));

                var principal = new GenericPrincipal(identity, roles.ToArray());

                Thread.CurrentPrincipal = principal;

                context.Validated(identity);

            }
            catch (System.Exception)
            {
                context.SetError("Acesso inválido", "Falha na autenticação");
            }
        }
    }
}
