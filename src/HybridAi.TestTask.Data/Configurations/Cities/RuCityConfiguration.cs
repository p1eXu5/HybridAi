using HybridAi.TestTask.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HybridAi.TestTask.Data.Configurations
{
    public class RuCityConfiguration : IEntityTypeConfiguration< RuCity >
    {
        public void Configure( EntityTypeBuilder< RuCity > builder )
        {
            builder.ToTable( "RuCities", "dbo" );

            builder.HasKey(e => new { e.ContinentName, e.CountryName, e.CityName });
            builder.HasOne(e => e.LocaleCode).WithMany(l => l.RuCities).HasForeignKey(e => e.LocaleCodeId);
        }
    }
}
