using HybridAi.TestTask.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HybridAi.TestTask.Data.Configurations.Cities
{
    public class PtBrCityConfiguration : IEntityTypeConfiguration< PtBrCity >
    {
        public void Configure( EntityTypeBuilder< PtBrCity > builder )
        {
            builder.ToTable( "PtBrCities", "dbo" );

            builder.HasKey(e => new { e.ContinentName, e.CountryName, e.CityName });
            builder.HasOne(e => e.LocaleCode).WithMany(l => l.PtBrCities).HasForeignKey(e => e.LocaleCodeId);
        }
    }
}
