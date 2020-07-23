using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NetCoreApi.Models{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Created).IsRequired();
            builder.Property(r => r.CreatedByIp).IsRequired();
            builder.Property(r => r.Token).IsRequired();
            builder.Property(r => r.Revoked).IsRequired(false);
            builder.Property(r => r.RevokedByIp).IsRequired(false);
            builder.Property(r => r.ReplacedByToken).IsRequired(false);
            builder.HasOne(r => r.User).WithMany(u => u.RefreshTokens).HasForeignKey(r => r.UserId);
        }
    }

}