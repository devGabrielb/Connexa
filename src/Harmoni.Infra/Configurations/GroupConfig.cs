using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Harmoni.Infra.Configurations
{
    public class GroupConfig : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("groups");
            builder.HasKey(fg => fg.Id);
            builder.Property(fg => fg.Id).IsRequired().ValueGeneratedNever();

            builder.Property(fg => fg.Name).IsRequired();
            builder.Property(fg => fg.Description).IsRequired();
            builder.Property(fg => fg.GroupPicture).IsRequired();
            builder.Property(fg => fg.CreatedAt).IsRequired();
            builder.Property(fg => fg.UpdatedAt).IsRequired();
            builder.Property(fg => fg.SubscriptionId).IsRequired();
            builder.Property("_maxMembers").IsRequired();
            builder.Property("_maxChores").IsRequired();
            builder.HasMany(g => g.Members).WithOne().HasForeignKey(mg => mg.GroupId);
            builder.HasMany(g => g.Chores).WithOne().HasForeignKey(c => c.GroupId).IsRequired(false);


        }
    }
}