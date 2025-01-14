using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Domain.Entities;
using Harmoni.Domain.Enums;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Harmoni.Infra.Configurations
{
    public class SubscriptionConfig : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable("subscriptions");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).IsRequired().ValueGeneratedNever();
            builder.Property(c => c.UserId).IsRequired();
            builder.Property(c => c.GroupPlan).IsRequired();
            builder.Property(c => c.CreatedAt).IsRequired();

            builder.HasMany(s => s.Groups).WithOne().HasForeignKey(g => g.SubscriptionId);
            builder.HasOne<User>().WithMany().HasForeignKey(c => c.UserId);

        }
    }
}