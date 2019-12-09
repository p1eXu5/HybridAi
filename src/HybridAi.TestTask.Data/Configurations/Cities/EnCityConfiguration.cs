﻿using HybridAi.TestTask.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HybridAi.TestTask.Data.Configurations
{
    public class EnCityConfiguration : IEntityTypeConfiguration< EnCity >
    {
        public void Configure( EntityTypeBuilder< EnCity > builder )
        {
            builder.ToTable( "EnCities", "dbo" );

            builder.HasKey( c => c.GeonameId );
            builder.Property( c => c.GeonameId ).HasColumnType( "int" ).IsRequired();

            builder.HasOne( c => c.CityLocation ).WithOne( cl => cl.EnCity ).HasForeignKey< CityLocation >( c => c.GeonameId );
            builder.HasOne(c => c.LocaleCode).WithMany(l => l.EnCities).HasForeignKey(c => c.LocaleCodeName);
        }
    }
}
