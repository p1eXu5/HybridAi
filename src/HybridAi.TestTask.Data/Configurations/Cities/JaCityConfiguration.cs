using HybridAi.TestTask.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HybridAi.TestTask.Data.Configurations.Cities
{
    public class JaCityConfiguration : IEntityTypeConfiguration< JaCity >
    {
        public void Configure( EntityTypeBuilder< JaCity > builder )
        {
            builder.ToTable( "JaCities", "dbo" );

            builder.HasKey(e => new { e.ContinentName, e.CountryName, e.CityName });
            builder.HasOne(e => e.LocaleCode).WithMany(l => l.JaCities).HasForeignKey(e => e.LocaleCodeId);
        }
    }
}
