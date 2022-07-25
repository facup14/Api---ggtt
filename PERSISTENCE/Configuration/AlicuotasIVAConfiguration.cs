using DATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PERSISTENCE.Configuration
{
    public class AlicuotasIVAConfiguration
    {
        public AlicuotasIVAConfiguration(EntityTypeBuilder<AlicuotasIVA> entity)
        {
            entity.HasKey(e => e.IdAlicuota);

            entity.Property(e => e.IdAlicuota)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Detalle)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Alicuota)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnType("money");

            entity.Property(e => e.NumeroCUIT)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.AlicuotaRecargo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnType("money");


        }
    }
}
