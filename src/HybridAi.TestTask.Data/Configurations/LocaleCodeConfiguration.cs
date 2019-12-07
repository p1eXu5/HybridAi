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
    public class LocaleCodeConfiguration : IEntityTypeConfiguration< LocaleCode >
    {
        public virtual void Configure(EntityTypeBuilder< LocaleCode > builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property( e => e.Id ).ValueGeneratedOnAdd();

            builder.HasMany( l => l.EnCities );

            builder.HasData( new[]
            {
                new LocaleCode { Id = 1, Name = "en" }, 
                new LocaleCode { Id = 2, Name = "de" }, 
                new LocaleCode { Id = 3, Name = "fr" }, 
                new LocaleCode { Id = 4, Name = "es" }, 
                new LocaleCode { Id = 5, Name = "ru" }, 
                new LocaleCode { Id = 6, Name = "ja" }, 
                new LocaleCode { Id = 7, Name = "zh-CN" }, 
                new LocaleCode { Id = 8, Name = "pt-BR" }, 
            } );
        }
    }
}
