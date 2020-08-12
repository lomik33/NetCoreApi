using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NetCoreApi.Models{
    public class ComidaConfiguration : IEntityTypeConfiguration<Comida>
    {
        public void Configure(EntityTypeBuilder<Comida> builder)
        {
            builder.Property(c => c.ComidaId).ValueGeneratedOnAdd();
            builder.HasKey( c=> c.ComidaId);
            builder.Property(c => c.NombreComida).IsRequired(true);
            builder.Property(c => c.TipoComida).IsRequired(true);
            builder.Property(c => c.PrecioComida).IsRequired(true).HasColumnType("decimal(18, 2)");
            builder.Property(c => c.DescripcionComida).IsRequired(false);
        }
    }
}