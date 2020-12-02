using Microsoft.EntityFrameworkCore;
using MyTunes_app.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTunes_APP.Infraestructure
{
    public class MyTunes_appDbContext : DbContext
    {

        public MyTunes_appDbContext(DbContextOptions options) :
            base(options)
            { }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Songs> Msongs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new AlbumConfiguration());
            modelBuilder.ApplyConfiguration(new SongsConfiguration());

            
        }




    }
}
