using HybridAi.TestTask.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HybridAi.TestTask.Data.Configurations.Cities
{
    public class EsCityConfiguration : IEntityTypeConfiguration< EsCity >
    {
        public void Configure( EntityTypeBuilder< EsCity > builder )
        {
            builder.ToTable( "EsCities", "dbo" );

            builder.HasKey( c => c.GeonameId );
            builder.Property( c => c.GeonameId ).HasColumnType( "int" ).IsRequired();

            builder.HasOne( c => c.CityLocation ).WithOne( cl => cl.EsCity ).HasForeignKey< CityLocation >( c => c.EsCityGeonameId );
            builder.HasOne(c => c.LocaleCode).WithMany(l => l.EsCities).HasForeignKey(c => c.LocaleCodeName);
        }
    }
}
