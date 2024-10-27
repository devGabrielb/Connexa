using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Connexa.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connexa.Infra.Configurations
{
    public class ChoreConfig : IEntityTypeConfiguration<Chore>
    {
        public void Configure(EntityTypeBuilder<Chore> builder)
        {
            builder.ToTable("chores");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Title).IsRequired();
            builder.Property(t => t.Description).IsRequired();
            builder.Property(t => t.DueDate).IsRequired();
            builder.Property(t => t.State).IsRequired();
            builder.Property(t => t.CreatedAt).IsRequired();
            builder.Property(t => t.UpdatedAt).IsRequired();
            builder.Property(t => t.GroupId);
            builder.Property(t => t.OwnerId).IsRequired();

            builder.HasMany(t => t.Comments).WithOne().HasForeignKey(c => c.ChoreId);



        }
    }
}