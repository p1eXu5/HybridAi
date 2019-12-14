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
    public class CityBlockIpv6Configuration : IEntityTypeConfiguration< CityBlockIpv6 >
    {
        public void Configure( EntityTypeBuilder< CityBlockIpv6 > builder )
        {
            builder.ToTable( "CityBlocksIpv6s", "dbo" );

            builder.HasKey( c => c.Network );
            builder.Property( c => c.Network ).HasColumnType( "varchar(47)" );

            builder.HasIndex( c => c.CityLocationGeonameId );

            builder.HasOne( c => c.CityLocation ).WithMany().HasForeignKey( c => c.CityLocationGeonameId );
        }
    }
}
