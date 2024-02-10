using Microsoft.EntityFrameworkCore;
using OrderHistory.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHistory.Domain.Context
{
    public class DBContext:DbContext
    {
        public DBContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Member> Member { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Member>(e => 
            {
                e.ToTable("Member");
                e.Property(x => x.Name).IsRequired(true)
                        .HasMaxLength(500)
                    .HasColumnType("nvarchar(500)");
            });
            modelBuilder.Entity<Product>(e =>
            {
                e.ToTable("Product");
                e.Property(x => x.Name).IsRequired(true)
                .HasMaxLength(500)
                    .HasColumnType("nvarchar(500)");
            });
            modelBuilder.Entity<Order>(e =>
            {
                e.ToTable("Order");
                e.HasOne(x => x.Product).WithMany(x => x.Orders);
                e.HasOne(x => x.Member).WithMany(x => x.Orders);
                e.Property(x => x.DateOrder).HasColumnType("smallDateTime");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
