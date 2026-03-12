using MChatBackend.Core.Domain.IdentityEntities;
using MChatBackend.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MChatBackend.Infrastrecture.DBContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Message configuration (Optional but recommended ⭐)
            builder.Entity<Message>()
                .HasIndex(m => new { m.SenderId, m.ReceiverId });

            builder.Entity<Message>()
                .Property(m => m.Content)
                .HasMaxLength(2000);
        }

        protected ApplicationDbContext()
        {

        }

    }
}
