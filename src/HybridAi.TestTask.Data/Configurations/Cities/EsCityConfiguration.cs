using HybridAi.TestTask.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HybridAi.TestTask.Data.Configurations
{
    public class EsCityConfiguration : IEntityTypeConfiguration< EsCity >
    {
        public void Configure( EntityTypeBuilder< EsCity > builder )
        {
            builder.ToTable( "EsCities", "dbo" );

            builder.HasKey(e => new { e.ContinentName, e.CountryName, e.CityName });
            builder.HasOne(e => e.LocaleCode).WithMany(l => l.EsCities).HasForeignKey(e => e.LocaleCodeId);
        }
    }
}
