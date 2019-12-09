using HybridAi.TestTask.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HybridAi.TestTask.Data.Configurations.Cities
{
    public class ZhCnCityConfiguration : IEntityTypeConfiguration< ZhCnCity >
    {
        public void Configure( EntityTypeBuilder< ZhCnCity > builder )
        {
            builder.ToTable( "ZhCnCities", "dbo" );

            builder.HasKey(e => new { e.ContinentName, e.CountryName, e.CityName });
            builder.HasOne(e => e.LocaleCode).WithMany(l => l.ZhCnCities).HasForeignKey(e => e.LocaleCodeId);
        }
    }
}
