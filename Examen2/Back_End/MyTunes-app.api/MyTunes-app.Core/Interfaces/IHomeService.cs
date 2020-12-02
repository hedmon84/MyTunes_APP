using Microsoft.AspNetCore.Mvc;
using MyTunes_app.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyTunes_app.Core.Interfaces
{
    public interface IHomeService
    {

       ServiceResult<IEnumerable<Album>> GetpopAlbum();
        ServiceResult<Album> Getbyid(int idalbum);

        ServiceResult<Album> RankAlbum(int albumid, int Rating);



        ServiceResult<Album> Buy_Song(int albumid, int songid,  bool buyclick);
        ServiceResult<Album> Buy_Album(int albumid, bool buyclick);

        ServiceResult<string> Profile();

    }
}
