using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Harmoni.Infra.Configurations
{
    public class ChoreConfig : IEntityTypeConfiguration<Chore>
    {
        public void Configure(EntityTypeBuilder<Chore> builder)
        {
            builder.ToTable("chores");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).IsRequired().ValueGeneratedNever();
            builder.Property(t => t.Title).IsRequired();
            builder.Property(t => t.Description).IsRequired();
            builder.Property(t => t.DueDate).IsRequired();
            builder.Property(t => t.State).IsRequired();
            builder.Property(t => t.CreatedAt).IsRequired();
            builder.Property(t => t.UpdatedAt).IsRequired();

            builder.HasOne<Group>().WithMany().HasForeignKey(c => c.GroupId).IsRequired(false);
            builder.HasOne<User>().WithMany().HasForeignKey(c => c.UserId).IsRequired(false);
            builder.HasOne<MemberGroup>().WithMany().HasForeignKey(c => c.AssignedBy).IsRequired(false);
            builder.HasOne<MemberGroup>().WithMany().HasForeignKey(c => c.AssignedTo).IsRequired(false);



        }
    }
}