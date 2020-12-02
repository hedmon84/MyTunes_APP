using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyTunes_app.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTunes_APP.Infraestructure
{
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {

            builder.HasKey(x => x.Idalbum);

            builder.Property(x => x.Idalbum).ValueGeneratedOnAdd();

            builder.Property(x => x.Album_name).IsRequired();

            builder.Property(x => x.Artist_name).IsRequired();

            builder.Property(x => x.Price).IsRequired();

            builder.Property(x => x.Style).IsRequired();

            builder.Property(x => x.Rating).IsRequired();

            builder.Property(x => x.Date_departure).IsRequired();

            builder.Property(x => x.Description).IsRequired();

            builder.Property(x => x.Img).IsRequired();

            builder.Property(x => x.Buyalbum).IsRequired();





        }
    }
}
