using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTunes_app.Core.Entities
{
    public class Album
    {
        public int Idalbum { get; set; }

        public string Album_name { get; set; }

        public string Artist_name { get; set; }

        public decimal Price { get; set; }

        public string Style { get; set; }

        public int Rating { get; set; }

        public double Duration { get; set; }

        public string Date_departure { get; set; }

        public string Description { get; set; }

        public string Img { get; set; }

        public bool Buyalbum { get; set; }

        public IEnumerable<Songs> Msongs { get; set; }


    }
}
