using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Connexa.Domain.Commons;
using Connexa.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Connexa.Infra.Persistence
{
    public class ConnexaContext : DbContext, IUnitOfWork
    {
        public ConnexaContext(
        DbContextOptions options
       )
        : base(options)
        {
        }

        public DbSet<Group> FamilyGroups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Chore> TaskItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConnexaContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}