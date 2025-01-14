using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Harmoni.Infra.Configurations
{
    public class MemberGroupConfig : IEntityTypeConfiguration<MemberGroup>
    {

        public void Configure(EntityTypeBuilder<MemberGroup> builder)
        {
            builder.ToTable("member_groups");
            builder.HasKey(mg => mg.Id);
            builder.Property(mg => mg.Id).IsRequired().ValueGeneratedNever();
            builder.Property(mg => mg.GroupId).IsRequired();
            builder.Property(mg => mg.UserId).IsRequired();
            builder.Property(mg => mg.IsAdmin).IsRequired();
            builder.Property(mg => mg.Status).IsRequired();

            builder.HasOne<User>().WithMany().HasForeignKey(mg => mg.UserId);
        }
    }
}