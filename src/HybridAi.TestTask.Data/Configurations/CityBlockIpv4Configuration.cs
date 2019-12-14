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
    public class CityBlockIpv4Configuration : IEntityTypeConfiguration< CityBlockIpv4 >
    {
        public void Configure( EntityTypeBuilder< CityBlockIpv4 > builder )
        {
            builder.ToTable( "CityBlocksIpv4s", "dbo" );

            builder.HasKey( c => c.Network );
            builder.Property( c => c.Network ).HasColumnType( "varchar(11)" );

            builder.HasIndex( c => c.CityLocationGeonameId );

            builder.HasOne( c => c.CityLocation ).WithMany().HasForeignKey( c => c.CityLocationGeonameId );
            builder.HasOne( c => c.CountryLocation ).WithMany().HasForeignKey( c => c.RegistredCountryGeonameId );
        }
    }
}
