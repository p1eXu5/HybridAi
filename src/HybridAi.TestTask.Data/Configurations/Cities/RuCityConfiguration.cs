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

            builder.HasKey( c => c.GeonameId );
            builder.Property( c => c.GeonameId ).HasColumnType( "int" ).IsRequired();

            builder.HasOne( c => c.CityLocation ).WithOne( cl => cl.RuCity ).HasForeignKey< CityLocation >( c => c.GeonameId );
            builder.HasOne(c => c.LocaleCode).WithMany(l => l.RuCities).HasForeignKey(c => c.LocaleCodeName);
        }
    }
}
