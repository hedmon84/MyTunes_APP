using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyTunes_APP.Infraestructure;
using MyTunes_app.Core.Entities;
using Newtonsoft.Json;

namespace MyTunes_app.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly MyTunes_appDbContext _myTunes_appDbContext;
        public HomeController(MyTunes_appDbContext myTunes_appDbContext)
        {
            _myTunes_appDbContext = myTunes_appDbContext;


        }

        //[HttpGet]
        //public IEnumerable<Album> Get()
        //{

        //    return _myTunes_appDbContext.Albums.Include(x=>x.Msongs);

        //}

        struct HomePage
        {
            public string Album_name;
            public string Artista;
            public decimal total;

        }


        // Mostrar los  10 mas populares 
        //Home route
        [HttpGet]

        public ActionResult<string> Get3()
        {



            var listAlbumId = _myTunes_appDbContext.Albums.Include(x => x.Msongs).Select(r=>r.Idalbum);
            var list_Albums = _myTunes_appDbContext.Albums.Include(x => x.Msongs).Where(x=>listAlbumId.Contains(x.Idalbum));
            var orderAlbums = list_Albums;


            var list2 = orderAlbums.ToList().Select(a => new Album
            {
                Album_name = a.Album_name,
                Buyalbum = a.Buyalbum,
                Artist_name = a.Artist_name,
                Date_departure = a.Date_departure,
                Description = a.Description,
                Idalbum = a.Idalbum,
                Img = a.Img,
                Msongs = a.Msongs,
                Price = Convert.ToDecimal(a.Msongs.Sum(x => x.Price) - 0.10),
                Rating = a.Msongs.Count() == 0 ? 0 : a.Msongs.Sum(x => x.Rating) / a.Msongs.Count(),
                Style = a.Style,

             


            });


            HomePage dbdata1 = new HomePage();

            List<HomePage> total = new List<HomePage>();

           var list1 = list2.OrderByDescending(x => x.Rating).Take(10);

            foreach ( var item in list1)
            {
                dbdata1.Album_name = item.Album_name;
                dbdata1.Artista = item.Artist_name;
                dbdata1.total = item.Price;

                total.Add(dbdata1);



            }

            var stringjson = JsonConvert.SerializeObject(total);


            return stringjson;
        }



        //Mostrar detalle de un Album

        [HttpGet]
        [Route("details/{albumid}")]
        public Album Get(int albumid)
        {


            var album_details = _myTunes_appDbContext.Albums.Include(x => x.Msongs).FirstOrDefault(x => x.Idalbum == albumid);


            return album_details;

        }


        //Darle Ranking al album

        [HttpGet]
        [Route("details/{albumid}/rank/{rankid}")]
        public Album Details(int albumid,int rankid)
        {


            var list_Albums = _myTunes_appDbContext.Albums.Include(x => x.Msongs).FirstOrDefault(x => x.Idalbum == albumid);


             if (rankid > 5)
            {
                throw new ArgumentException($"Ranking has to be [1-5] .");

            } else
            {
                list_Albums.Rating = rankid;


                _myTunes_appDbContext.Albums.Update(list_Albums);
                _myTunes_appDbContext.SaveChanges();


                return list_Albums;

            }


        }





        //Comprar Cancione de un album

        [HttpPost]
        [Route("details/{albumid}/buy/song/{songid}")]
        public Album Buy_Song(int albumid, int songid , [FromBody] bool buyclick)
        {


            var list_Albums = _myTunes_appDbContext.Albums.Include(x => x.Msongs).FirstOrDefault(x => x.Idalbum == albumid);

            foreach( var data in list_Albums.Msongs)
            {

                if (data.Idsong == songid)
                {
                    data.Buyclick = buyclick;
                } 

            }

            _myTunes_appDbContext.Albums.Update(list_Albums);
            _myTunes_appDbContext.SaveChanges();




            return list_Albums;

        }



        // Buy album
        [HttpPost]
        [Route("details/{albumid}/buy/album")]
        public Album Buy_Album(int albumid,[FromBody] bool buyclick)
        {


            var list_Albums = _myTunes_appDbContext.Albums.Include(x => x.Msongs).FirstOrDefault(x => x.Idalbum == albumid);


            list_Albums.Buyalbum = buyclick;
            foreach (var data in list_Albums.Msongs)
            {

                    data.Buyclick = buyclick;
                

            }

            _myTunes_appDbContext.Albums.Update(list_Albums);
            _myTunes_appDbContext.SaveChanges();




            return list_Albums;

        }



        struct MyAlbum
        {

            public string Nombre;
            public string Artista;
            public double duracion;

        }
        public ICollection<Album> List;


        //Perfil
        [HttpGet]
        [Route("profile")]
        public ActionResult<string> Profile()
        {
            List = new List<Album>();

            List<MyAlbum> Album_Pucharse = new List<MyAlbum>();

            MyAlbum Prof_List = new MyAlbum();

            var myalbums = _myTunes_appDbContext.Albums.Include(x => x.Msongs);


            foreach (var items in myalbums)
            {

                if(items.Buyalbum == true)
                {
                    Prof_List.Nombre = items.Album_name;
                    Prof_List.Artista = items.Artist_name;

                    foreach (var item2 in items.Msongs)
                    {
                        if (item2.Buyclick == true)
                        {

                            Prof_List.duracion += item2.Duration;
                        }
                    }


                    Album_Pucharse.Add(Prof_List);


                }
                else
                {
                    Prof_List.Nombre = items.Album_name;
                    Prof_List.Artista = items.Artist_name;



                    foreach(var item2 in items.Msongs)
                    {
                        if(item2.Buyclick == true)
                        {
                            Prof_List.duracion += item2.Duration;
                            Album_Pucharse.Add(Prof_List);
                        }
                    }

                }

            }

            string stringjson = JsonConvert.SerializeObject(Album_Pucharse);

            return stringjson;
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
