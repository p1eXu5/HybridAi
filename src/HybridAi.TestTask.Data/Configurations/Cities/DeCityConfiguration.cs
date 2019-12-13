using HybridAi.TestTask.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HybridAi.TestTask.Data.Configurations.Cities
{
    public class DeCityConfiguration : IEntityTypeConfiguration< DeCity >
    {
        public void Configure( EntityTypeBuilder< DeCity > builder )
        {
            builder.ToTable( "DeCities", "dbo" );

            builder.HasKey( c => c.GeonameId );
            builder.Property( c => c.GeonameId ).HasColumnType( "int" ).IsRequired();

            builder.HasOne( c => c.CityLocation ).WithOne( cl => cl.DeCity ).HasForeignKey< CityLocation >( c => c.DeCityGeonameId );
            builder.HasOne(c => c.LocaleCode).WithMany(l => l.DeCities).HasForeignKey(c => c.LocaleCodeName);
        }
    }
}
