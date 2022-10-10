using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveMQ_PoC.ConsumerA.Data.Entities;
using ActiveMQ_PoC.Shared.Interfaces.Events;
using Microsoft.EntityFrameworkCore;

namespace ActiveMQ_PoC.ConsumerA.Data.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<TransportOrderDoc> TransportOrderDocs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransportOrderDoc>()
                .Property(b => b.TransportOrder)
                .HasColumnType("jsonb");

            modelBuilder.Entity<TransportOrderDoc>()
                .Property(b => b.OriginalEvent)
                .HasColumnType("json");

            modelBuilder.Entity<TransportOrderDoc>()
                .HasIndex(t => new {t.TransportOrder})
                .HasMethod("gin");
        }
    }
}
