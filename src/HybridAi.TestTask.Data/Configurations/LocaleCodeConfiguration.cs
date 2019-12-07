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
        }
    }
}
