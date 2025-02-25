﻿using Microsoft.AspNetCore.Mvc;
using MyTunes_app.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using MyTunes_app.Core.Entities;

namespace MyTunes_app.Core.Services
{
    public class HomeService : IHomeService
    {
        public ICollection<Album> List;
        public IRepository<Album> _albumsRepositories;


        public HomeService(IRepository<Album> albumsRepositories)
        {
            _albumsRepositories = albumsRepositories;
           
        }



        public ServiceResult<Album> Buy_Album(int albumid,  bool buyclick)
        {
            var list_Albums = _albumsRepositories.GetAllAlbumWithSogs().FirstOrDefault(x => x.Idalbum == albumid);


            list_Albums.Buyalbum = buyclick;
            foreach (var data in list_Albums.Msongs)
            {

                data.Buyclick = buyclick;


            }


            _albumsRepositories.Actualizar(list_Albums);


            return ServiceResult<Album>.SuccessResult(list_Albums);
        }

        public ServiceResult<Album> Buy_Song(int albumid, int songid,bool buyclick)
        {

            var list_Albums = _albumsRepositories.GetAllAlbumWithSogs().FirstOrDefault(x => x.Idalbum == albumid);

            foreach (var data in list_Albums.Msongs)
            {

                if (data.Idsong == songid)
                {
                    data.Buyclick = buyclick;
                }

            }


            _albumsRepositories.Actualizar(list_Albums);


            return ServiceResult<Album>.SuccessResult(list_Albums);



        }

        public ServiceResult<Album> Getbyid(int idalbum)
        {
            var List_albums = _albumsRepositories.GetAllAlbumWithSogs().FirstOrDefault(x => x.Idalbum == idalbum);


            return ServiceResult<Album>.SuccessResult(List_albums);
        }

        //struct HomePage
        //{
        //    public string Album_name;
        //    public string Artista;
        //    public decimal total;

        //}
        public ServiceResult<IEnumerable<Album>> GetpopAlbum()
        {
            var listalbumid = _albumsRepositories.GetAllAlbumWithSogs().Select(r => r.Idalbum);
            var list_Albums = _albumsRepositories.GetAllAlbumWithSogs().Where(x => listalbumid.Contains(x.Idalbum));
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
                Duration = Math.Round(a.Msongs.Sum(x => x.Duration), 1),
                Style = a.Style,



            });


            //HomePage dbdata1 = new HomePage();

            //List<HomePage> total = new List<HomePage>();

            var list1 = list2.OrderByDescending(x => x.Rating).Take(10);

            //foreach (var item in list1)
            //{
            //    dbdata1.Album_name = item.Album_name;
            //    dbdata1.Artista = item.Artist_name;
            //    dbdata1.total = item.Price;

            //    total.Add(dbdata1);



            //}





            return ServiceResult<IEnumerable<Album>>.SuccessResult(list1); 

        }

  

        public ServiceResult<IEnumerable<Album>> Profile()
        {
            List = new List<Album>();

            List<Album> Album_Pucharse = new List<Album>();

           


            var myalbums = _albumsRepositories.GetAllAlbumWithSogs();
            bool flag = false;


            foreach (var items in myalbums)
            {

                if (items.Buyalbum == true)
                {
                    
                    foreach (var item2 in items.Msongs)
                    {
                        if (item2.Buyclick == true)
                        {

                            items.Duration += item2.Duration;

                        }
                    }


                    Album_Pucharse.Add(items);


                }
                else
                {
           

                    foreach (var item2 in items.Msongs)
                    {
                        if (item2.Buyclick == true)
                        {
                            items.Duration += item2.Duration;
                            Album_Pucharse.Add(items);
                        }
                    }

                    if(flag == true)
                    {
                        Album_Pucharse.Add(items);
                        flag = false;

                    }

                }

            }





            return ServiceResult<IEnumerable<Album>>.SuccessResult(Album_Pucharse);
        }

        public ServiceResult<Album> RankAlbum(int albumid, int Rating)
        {
            var list_Albums = _albumsRepositories.GetAllAlbumWithSogs().FirstOrDefault(x => x.Idalbum == albumid);


            if (Rating > 5)
            {
                throw new ArgumentException($"Ranking has to be [1-5] .");

            }
            else
            {
                list_Albums.Rating = Rating;
                _albumsRepositories.Actualizar(list_Albums);



                return ServiceResult<Album>.SuccessResult(list_Albums);


            }

        }
    }
}
