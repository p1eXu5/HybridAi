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
            builder.HasKey(e => e.Name);
            builder.Property( e => e.Name ).HasColumnType( "varchar(8)" ).IsRequired();

            builder.HasMany( l => l.EnCities );

            builder.HasData( new[]
            {
                new LocaleCode("en" ), 
                new LocaleCode("de" ), 
                new LocaleCode("fr" ), 
                new LocaleCode("es" ), 
                new LocaleCode("ru" ), 
                new LocaleCode("ja" ), 
                new LocaleCode("zh-CN" ), 
                new LocaleCode("pt-BR" ), 
            } );
        }
    }
}
