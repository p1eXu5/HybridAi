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

            builder.HasKey( c => c.GeonameId );
            builder.Property( c => c.GeonameId ).HasColumnType( "int" ).IsRequired();

            builder.HasOne( c => c.CityLocation ).WithOne( cl => cl.ZhCnCity ).HasForeignKey< CityLocation >( c => c.ZhCnCityGeonameId );
            builder.HasOne(c => c.LocaleCode).WithMany(l => l.ZhCnCities).HasForeignKey(c => c.LocaleCodeName);
        }
    }
}
