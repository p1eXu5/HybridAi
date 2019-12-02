using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HybridAi.TestTask.Data.Configurations
{
    public class CityLocationConfiguraion : IEntityTypeConfiguration< CityLocation >
    {
        public void Configure( EntityTypeBuilder< CityLocation > builder )
        {
            builder.ToTable( "CityLocations", "dbo" );

            builder.HasKey( c => c.GeonameId );
            builder.Property( c => c.GeonameId ).HasColumnType( "int4" );

            builder.HasMany( c => c.CityBlockIpv4Collection ).WithOne( c => c.CityLocation );
            builder.HasMany( c => c.CityBlockIpv6Collection ).WithOne( c => c.CityLocation );
        }
    }
}
