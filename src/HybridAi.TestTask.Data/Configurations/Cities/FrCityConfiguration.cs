using HybridAi.TestTask.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HybridAi.TestTask.Data.Configurations.Cities
{
    public class FrCityConfiguration : IEntityTypeConfiguration< FrCity >
    {
        public void Configure( EntityTypeBuilder< FrCity > builder )
        {
            builder.ToTable( "FrCities", "dbo" );

            builder.HasKey(e => new { e.ContinentName, e.CountryName, e.CityName });
            builder.HasOne(e => e.LocaleCode).WithMany(l => l.FrCities).HasForeignKey(e => e.LocaleCodeId);
        }
    }
}
