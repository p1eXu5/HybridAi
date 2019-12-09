using HybridAi.TestTask.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HybridAi.TestTask.Data.Configurations
{
    public class EnCityConfiguration : IEntityTypeConfiguration< EnCity >
    {
        public void Configure( EntityTypeBuilder< EnCity > builder )
        {
            builder.ToTable( "EnCities", "dbo" );

            builder.HasKey(e => new { e.ContinentName, e.CountryName, e.CityName });
            builder.HasOne(e => e.LocaleCode).WithMany(l => l.EnCities).HasForeignKey(e => e.LocaleCodeId);
        }
    }
}
