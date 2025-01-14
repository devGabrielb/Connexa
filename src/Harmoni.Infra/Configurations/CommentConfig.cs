using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Harmoni.Infra.Configurations
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("comments");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).IsRequired().ValueGeneratedNever();

            builder.Property(c => c.Content).IsRequired();
            builder.Property(c => c.UserId).IsRequired();
            builder.Property(c => c.ChoreId).IsRequired();
            builder.Property(c => c.CreatedAt).IsRequired();

            builder.HasOne<User>().WithMany().HasForeignKey(c => c.UserId);
            builder.HasOne<Chore>().WithMany().HasForeignKey(c => c.ChoreId);
        }
    }
}