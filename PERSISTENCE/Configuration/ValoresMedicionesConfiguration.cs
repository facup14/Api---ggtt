using DATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PERSISTENCE.Configuration
{
    public class ValoresMedicionesConfiguration
    {
        public ValoresMedicionesConfiguration(EntityTypeBuilder<ValoresMediciones> entity)
        {
            entity.HasKey(e => e.IdValorMedicion);

            entity.Property(e => e.ValorMedicion)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Obs)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}
