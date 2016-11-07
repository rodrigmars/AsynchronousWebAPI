using System.Collections.Generic;
using System.Web.Http;
using WorldMusic.Api.ViewModels;

namespace WorldMusic.Api.Controllers.V1
{
    public class ListenersController : ApiController
    {
        [Authorize(Roles = "User")]
        public string Get()
        {
            return User.Identity.Name;
        }

        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetClients()
        {
            var listeners = new List<Listener> {
                new Listener { ListenerId= 251 , Cod = new System.Guid(), Login = "US-4521" },
                new Listener { ListenerId= 350 , Cod = new System.Guid(), Login = "US-1521" },
                new Listener { ListenerId= 445 , Cod = new System.Guid(), Login = "US-7845" },
            };

            return Ok(listeners);
        }
    }
}
