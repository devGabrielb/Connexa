using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Connexa.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connexa.Infra.Configurations
{
    public class GroupConfig : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("groups");
            builder.HasKey(fg => fg.Id);
            builder.Property(fg => fg.Name).IsRequired();
            builder.Property(fg => fg.Description).IsRequired();
            builder.Property(fg => fg.CreatedAt).IsRequired();
            builder.Property(fg => fg.UpdatedAt).IsRequired();
            builder.Property(fg => fg.OwnerId).IsRequired();
            builder.HasMany(fg => fg.Members).WithOne().HasForeignKey(mg => mg.GroupId);
            builder.HasMany(fg => fg.Chores).WithOne().HasForeignKey(t => t.GroupId);

        }
    }
}