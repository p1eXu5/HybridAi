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

            builder.HasKey( c => c.GeonameId );
            builder.Property( c => c.GeonameId ).HasColumnType( "int" ).IsRequired();

            builder.HasOne( c => c.CityLocation ).WithOne( cl => cl.FrCity ).HasForeignKey< CityLocation >( c => c.FrCityGeonameId );
            builder.HasOne(c => c.LocaleCode).WithMany(l => l.FrCities).HasForeignKey(c => c.LocaleCodeName);
        }
    }
}
