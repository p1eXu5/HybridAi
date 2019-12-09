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

            builder.HasKey( c => c.GeonameId );
            builder.Property( c => c.GeonameId ).HasColumnType( "int" ).IsRequired();

            builder.HasOne( c => c.CityLocation ).WithOne( cl => cl.PtBrCity ).HasForeignKey< CityLocation >( c => c.GeonameId );
            builder.HasOne(c => c.LocaleCode).WithMany(l => l.PtBrCities).HasForeignKey(c => c.LocaleCodeName);
        }
    }
}
