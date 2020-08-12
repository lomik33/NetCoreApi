using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NetCoreApi.Models{
    public class UnidadMedidaConfiguration : IEntityTypeConfiguration<UnidadMedida>
    {
        public void Configure(EntityTypeBuilder<UnidadMedida> builder)
        {
            builder.ToTable("UnidadesMedida");
            builder.Property(u => u.UnidadDeMedidaId).ValueGeneratedOnAdd();
            builder.HasKey(u => u.UnidadDeMedidaId);
            builder.Property(u => u.NombreUnidad).HasMaxLength(150).IsRequired(true);
            builder.Property(u => u.AbreviacionUnidad).HasMaxLength(10).IsRequired(true);
            builder.Property(u => u.TipoUnidad).IsRequired(true);
        }
    }
}