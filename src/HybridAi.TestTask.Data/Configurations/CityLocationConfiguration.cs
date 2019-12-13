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
    public class CityLocationConfiguration : IEntityTypeConfiguration< CityLocation >
    {
        public void Configure( EntityTypeBuilder< CityLocation > builder )
        {
            builder.ToTable( "CityLocations", "dbo" );

            builder.HasKey( c => c.GeonameId );
            builder.Property( c => c.GeonameId ).HasColumnType( "int4" );

            builder.HasMany( c => c.CityBlockIpv4Collection ).WithOne( c => c.CityLocation );
            builder.HasMany( c => c.CityBlockIpv6Collection ).WithOne( c => c.CityLocation );

            builder.HasOne( c => c.EnCity ).WithOne( cl => cl.CityLocation ).HasForeignKey< EnCity >( c => c.GeonameId );
            builder.HasOne( c => c.RuCity ).WithOne( cl => cl.CityLocation ).HasForeignKey< RuCity>( c => c.GeonameId );
            builder.HasOne( c => c.DeCity ).WithOne( cl => cl.CityLocation ).HasForeignKey< DeCity >( c => c.GeonameId );
            builder.HasOne( c => c.FrCity ).WithOne( cl => cl.CityLocation ).HasForeignKey< FrCity >( c => c.GeonameId );
            builder.HasOne( c => c.EsCity ).WithOne( cl => cl.CityLocation ).HasForeignKey< EsCity >( c => c.GeonameId );
            builder.HasOne( c => c.JaCity ).WithOne( cl => cl.CityLocation ).HasForeignKey< JaCity >( c => c.GeonameId );
            builder.HasOne( c => c.PtBrCity ).WithOne( cl => cl.CityLocation ).HasForeignKey< PtBrCity >( c => c.GeonameId );
            builder.HasOne( c => c.ZhCnCity ).WithOne( cl => cl.CityLocation ).HasForeignKey< ZhCnCity >( c => c.GeonameId );
        }
    }
}
