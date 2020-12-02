using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTunes_app.Core.Entities
{
    public class Songs
    {

        public int Idsong { get; set; }


        public string Name { get; set; }

        public string Artist { get; set; }

        public double Price { get; set; }

        public double Duration { get; set; }

        public int Rating { get; set; }

        public bool Buyclick { get; set; }

        public int Albumidalbum { get; set; }
    }
}
