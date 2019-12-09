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

            builder.HasKey( c => c.GeonameId );
            builder.Property( c => c.GeonameId ).HasColumnType( "int" ).IsRequired();

            builder.HasOne( c => c.CityLocation ).WithOne( cl => cl.JaCity ).HasForeignKey< CityLocation >( c => c.GeonameId );
            builder.HasOne(c => c.LocaleCode).WithMany(l => l.JaCities).HasForeignKey(c => c.LocaleCodeName);
        }
    }
}
