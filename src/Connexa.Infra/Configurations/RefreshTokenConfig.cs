using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Connexa.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connexa.Infra.Configurations
{
    public class RefreshTokenConfig : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("refresh_tokens");
            builder.HasKey(r => r.Id);
            builder.Property(rt => rt.Token)
                .IsRequired();
            builder.Property(rt => rt.Expires)
            .IsRequired();
            builder.Property(rt => rt.Created)
                .IsRequired();
        }
    }
}