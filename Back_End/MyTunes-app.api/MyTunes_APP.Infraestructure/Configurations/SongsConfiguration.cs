using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyTunes_app.Core.Entities;

namespace MyTunes_APP.Infraestructure
{
    public class SongsConfiguration : IEntityTypeConfiguration<Songs>
    {
        public void Configure(EntityTypeBuilder<Songs> builder)
        {
            builder.HasKey(x => x.Idsong);

            builder.Property(x => x.Idsong).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.Artist).IsRequired();

            builder.Property(x => x.Price).IsRequired();

            builder.Property(x => x.Duration).IsRequired();

            builder.Property(x => x.Rating).IsRequired();

            builder.Property(x => x.Buyclick).IsRequired();
        }
    }
}
