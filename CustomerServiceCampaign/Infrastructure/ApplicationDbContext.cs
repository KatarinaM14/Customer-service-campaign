using Domain.Models.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<FavoriteColor> FavoriteColors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
               .HasOne( c => c.Home)
               .WithMany()
               .HasForeignKey(c => c.HomeId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
               .HasOne(office => office.Office)
               .WithMany()
               .HasForeignKey(c => c.OfficeId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
               .HasMany(color => color.FavoriteColors)
               .WithOne();

            base.OnModelCreating(modelBuilder);
        }
    }
}
