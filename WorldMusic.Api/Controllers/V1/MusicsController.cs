﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using WorldMusic.Domain.Entities;
using WorldMusic.Domain.Interfaces.Repositories;

namespace WorldMusic.Api.V1.Controllers
{
    [RoutePrefix("api/v1/musics")]
    public class MusicsController : ApiController
    {
        private readonly IMusicRepository _repository;

        private readonly IUnitOfWork _uow;

        public MusicsController(IUnitOfWork uow, IMusicRepository repository)
        {
            _uow = uow;
            _repository = repository;
        }

        [Route(""), HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                var music = _repository.GetAll("SELECT * FROM MUSICS");

                _repository.Close();

                music = _repository.GetAll("SELECT * FROM MUSICS");

                return Ok(music);

            }
            catch (System.Exception)
            {

                throw;
            }
            finally {
                
                _repository.Close();
            }
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

        [Route("remove/{id:int}/inactive/{status:bool}/musics"), HttpGet]
        public IHttpActionResult RemoveTrackById(int Id, bool status)
        {

            if (Id == 0) return NotFound();

            var tran = _uow.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

            var err = 0;

            var remove = false;

            try
            {
                remove = _uow.MusicRepository.RemoveInactiveMusic(new { MusicId = Id, IsActive = status }, tran);

                tran.Commit();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);

                tran.Rollback();

                err = 1;
            }
            finally
            {
                _uow.Dispose();
            }

            if (err > 0) return Content(HttpStatusCode.NotModified, "Erro ao tentar excluir registro.");

            return Ok(remove);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Post(Music music)
        {

            if (music == null) return NotFound();

            var tran = _uow.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

            var err = 0;

            var add = false;

            try
            {
                add = _uow.MusicRepository.Add(music, tran);

                
                
                tran.Commit();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);

                tran.Rollback();

                err = 1;
            }
            finally
            {
                _uow.Dispose();
            }

            if (err > 0) return Content(HttpStatusCode.NotModified, "Erro ao tentar excluir registro.");

            return Ok(add);

        }

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