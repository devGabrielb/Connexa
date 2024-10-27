using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Connexa.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connexa.Infra.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name).IsRequired();
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.Password).IsRequired();
            builder.HasMany(u => u.FamilyGroups).WithOne().HasForeignKey(fg => fg.OwnerId);
            builder.HasMany(u => u.Tasks).WithOne().HasForeignKey(t => t.OwnerId);
            builder.HasMany(u => u.Comments).WithOne().HasForeignKey(c => c.UserId);

            //builder.HasOne<RefreshToken>().WithOne().HasForeignKey<RefreshToken>(r => r.UserId);

        }
    }
}