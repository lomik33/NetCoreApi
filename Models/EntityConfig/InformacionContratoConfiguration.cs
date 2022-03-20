using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NetCoreApi.Models{
    public class InformacionContratoConfiguration : IEntityTypeConfiguration<InformacionContrato>
    {
        public void Configure(EntityTypeBuilder<InformacionContrato> builder)
        {
            builder.HasKey(e => e.InformacionContratoId);
            builder.Property(e => e.Puesto).HasMaxLength(250).IsRequired(true);
            builder.Property(e => e.PuestoDescripcion).HasMaxLength(5000).IsRequired(false);
            builder.Property(e => e.TipoContrato).IsRequired(true);
            builder.Property(e => e.FechaContratacion).IsRequired(true);
            builder.Property(e => e.FechaTermino).IsRequired(false);
            builder.Property(e => e.Salario).HasColumnType("decimal (18, 2)").IsRequired(true);
                       builder.Property(e => e.CreatedAt).IsRequired(true);
            builder.Property(e => e.ModifiedAt).IsRequired(false);
            builder.Property(e => e.IsDeleted).IsRequired(true);
            builder.Property(e => e.DateDeleted).IsRequired(false);
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}