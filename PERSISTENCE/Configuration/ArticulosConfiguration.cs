using DATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PERSISTENCE.Configuration
{
    public class ArticulosConfiguration
    {

        public ArticulosConfiguration(EntityTypeBuilder<Articulos> entity)
        {
            entity.HasKey(e => e.IdArticulo);

            entity.Property(e => e.IdArticulo).HasColumnName("idArticulo");

            entity.Property(e => e.CodigoFabrica)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Costo).HasColumnType("money");

            entity.Property(e => e.DetalleArticulo)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Obs).IsUnicode(false);
        }

    }
}
