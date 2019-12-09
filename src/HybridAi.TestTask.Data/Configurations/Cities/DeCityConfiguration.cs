using HybridAi.TestTask.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HybridAi.TestTask.Data.Configurations
{
    public class DeCityConfiguration : IEntityTypeConfiguration< DeCity >
    {
        public void Configure( EntityTypeBuilder< DeCity > builder )
        {
            builder.ToTable( "DeCities", "dbo" );

            builder.HasKey(e => new { e.ContinentName, e.CountryName, e.CityName });
            builder.HasOne(e => e.LocaleCode).WithMany(l => l.DeCities).HasForeignKey(e => e.LocaleCodeId);
        }
    }
}
