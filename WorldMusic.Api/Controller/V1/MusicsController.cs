using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WorldMusic.Domain.Entities;
using WorldMusic.Domain.Interfaces.Repositories;

namespace WorldMusic.Api.V1.Controller
{
    [RoutePrefix("api/v{version:int}/musics")]
    public class MusicsController : ApiController
    {
        private readonly IMusicRepository _repository;

        public MusicsController(IMusicRepository repository)
        {
            _repository = repository;
        }

        [Route(""), HttpGet]
        public IHttpActionResult GetAll()
        {
            var musics = _repository.GetAll("select * from Musics");

            return Ok(musics);
        }

        [Route("track/{id:int}/musics"), HttpGet]
        public IHttpActionResult GetTrackId(int id)
        {
            var musics = new List<Music>
            {
                new Music{ Title = "1. Allure-2521", Track = 1, IsActive = true, IDProcess = 2521 }
            };

            var track = musics.FirstOrDefault(m => m.Track == id);

            if (track == null) return NotFound(); // Returns a NotFoundResult

            return Ok(track);
        }

        //[HttpPost, Route("")]
        //public void Post(Music music)
        //{

        //}

        //[HttpPut, Route("")]
        //public void Put(Music music)
        //{

        //}

        //[HttpDelete, Route("")]
        //public void Delete(int id)
        //{

        //}
    }
}