using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyTunes_APP.Infraestructure;
using Newtonsoft.Json;
using MyTunes_app.Core.Interfaces;
using MyTunes_app.Core.Enums;
using MyTunes_app.Core.Entities;

namespace MyTunes_app.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly IHomeService _homeService;
        public HomeController(IHomeService homeService)
        {

            _homeService = homeService;




        }

        //[HttpGet]
        //public IEnumerable<Album> Get()
        //{

        //    return _myTunes_appDbContext.Albums.Include(x=>x.Msongs);

        //}

   


        // Mostrar los  10 mas populares 
        //Home route
        [HttpGet]
        public ActionResult<string> Get()
        {

            var ServiceResult = _homeService.GetpopAlbum();
            if(ServiceResult.ResponseCode != ResponseCode.Success)
                return BadRequest(ServiceResult.Error);

            return Ok(ServiceResult.Result);
            

        
        }



        ////Mostrar detalle de un Album

        [HttpGet]
        [Route("details/{albumid}")]
        public ActionResult<Album> GetAlb(int albumid)
        {

            var ServiceResult = _homeService.Getbyid(albumid);
            if (ServiceResult.ResponseCode != ResponseCode.Success)
                return BadRequest(ServiceResult.Error);

            return Ok(ServiceResult.Result);



        }


        ////Darle Ranking al album

        [HttpPost]
        [Route("details/{albumid}/rank")]
        public ActionResult<Album> Details(int albumid, [FromBody]  int Rating)
        {


            var ServiceResult = _homeService.RankAlbum(albumid, Rating);
            if (ServiceResult.ResponseCode != ResponseCode.Success)
                return BadRequest(ServiceResult.Error);

            return Ok(ServiceResult.Result);



        }





        ////Comprar Cancione de un album

        [HttpPost]
        [Route("details/{albumid}/buy/song/{songid}")]
        public ActionResult<Album> Buy_Song(int albumid, int songid, [FromBody] bool buyclick)
        {


            var ServiceResult = _homeService.Buy_Song(albumid, songid, buyclick);
            if (ServiceResult.ResponseCode != ResponseCode.Success)
                return BadRequest(ServiceResult.Error);

            return Ok(ServiceResult.Result);
        }



        //// Buy album
        [HttpPost]
        [Route("details/{albumid}/buy/album")]
        public ActionResult<Album> Buy_Album(int albumid, [FromBody] bool buyclick)
        {



            var ServiceResult = _homeService.Buy_Album(albumid,buyclick);
            if (ServiceResult.ResponseCode != ResponseCode.Success)
                return BadRequest(ServiceResult.Error);

            return Ok(ServiceResult.Result);
        }



        //struct MyAlbum
        //{

        //    public string Nombre;
        //    public string Artista;
        //    public double duracion;

        //}
        //public ICollection<Album> List;


        ////Perfil
        [HttpGet]
        [Route("profile")]
        public ActionResult<string> Profile()
        {
            var ServiceResult = _homeService.Profile();
            if (ServiceResult.ResponseCode != ResponseCode.Success)
                return BadRequest(ServiceResult.Error);

            return Ok(ServiceResult.Result);
        }







        //Obtener 1 elemento por id
        //Query string para filtrar 

        //[HttpGet]
        //[Route("{Albumid}")]
        //public Album Get(int albumid)
        //{

        //    for (int i = 0; i < Albums.Length; i++)
        //    {
        //        if (Albums[i].idalbum == albumid)
        //        {
        //            return Albums[i];
        //        }
        //    }

        //    return null;

        //}


        //Post songs






    }
}
