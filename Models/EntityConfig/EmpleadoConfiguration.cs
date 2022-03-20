using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NetCoreApi.Models{
    public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            builder.Property(e => e.EmpleadoId).ValueGeneratedOnAdd();
            builder.HasKey(e => e.EmpleadoId);
            builder.Property(e => e.Nombre).HasMaxLength(250).IsRequired(true);
            builder.Property(e => e.ApellidoPaterno).HasMaxLength(150).IsRequired(true);
            builder.Property(e => e.ApellidoMaterno).HasMaxLength(150).IsRequired(true);
            builder.Property(e => e.FechaNacimiento).IsRequired(true);
            builder.Property(e => e.Genero).HasMaxLength(30).IsRequired(true);
            builder.Property(e => e.Rfc).HasMaxLength(20).IsRequired(true);
            builder.Property(e => e.Nss).HasMaxLength(20).IsRequired(true);
            builder.Property(e => e.EstadoCivil).IsRequired(true);
            builder.Property(e => e.CreatedAt).IsRequired(true);
            builder.Property(e => e.ModifiedAt).IsRequired(false);
            builder.Property(e => e.IsDeleted).IsRequired(true);
            builder.Property(e => e.DateDeleted).IsRequired(false);
            builder.HasOne(e => e.InformacionContrato).WithOne().HasForeignKey<InformacionContrato>(e => e.InformacionContratoId);
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}