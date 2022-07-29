using DATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace PERSISTENCE.Configuration
{
    public class RepuestosConfiguration
    {

        public RepuestosConfiguration(EntityTypeBuilder<Repuestos> entity)
        {
            entity.HasKey(e => e.IdRepuesto);

            entity.HasIndex(e => new { e.IdRepuesto, e.Detalle, e.CodRepuesto })
                .HasName("Repuestos_IdRepuestoDetalleCodRepuesto");

            entity.Property(e => e.CodArticuloTango)
                .HasColumnName("COD_ArticuloTANGO")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CodCwa)
                .HasColumnName("CodCWA")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CodRepuesto)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CodigoBarras)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Descripcion).IsUnicode(false);

            entity.Property(e => e.Detalle)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.IdRepuestoCwa).HasColumnName("IdRepuestoCWA");

            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.NombreImagen)
                .HasMaxLength(600)
                .IsUnicode(false);

            entity.Property(e => e.NroParte)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.NroSerie)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Obs).IsUnicode(false);

            entity.Property(e => e.PorcentajeGananciaAplicada).HasColumnType("money");

            entity.Property(e => e.Precio).HasColumnType("money");

            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.TipoCwa)
                .HasColumnName("TipoCWA")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.UnidadMedida)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(d => d.IdCodigo);
            //.WithMany(p => p.Repuestos)
            //.HasForeignKey(d => d.IdCodigo)
            //.HasConstraintName("FK_Repuestos_CodigosLista");

            entity.Property(d => d.IdMarcaRepuesto);
            //.WithMany(p => p.Repuestos)
            //.HasForeignKey(d => d.IdMarcaRepuesto)
            //.HasConstraintName("FK_Repuestos_MarcasRepuestos");

            entity.Property(d => d.IdProveedor);
            //.WithMany(p => p.Repuestos)
            //.HasForeignKey(d => d.IdProveedor)
            //.HasConstraintName("FK_Repuestos_Proveedores");

            entity.Property(d => d.IdSubRubroRepuesto);
                //.WithMany(p => p.Repuestos)
                //.HasForeignKey(d => d.IdSubRubroRepuesto)
                //.HasConstraintName("FK_Repuestos_SubRubrosRepuestos");

            entity.HasOne(d => d.idUnidadDeMedida)
                .WithMany(p => p.Repuestos)
                .HasForeignKey(d => d.IdUnidadDeMedida)
                .HasConstraintName("FK_Repuestos_UnidadesDeMedida");
        }

    }
}
