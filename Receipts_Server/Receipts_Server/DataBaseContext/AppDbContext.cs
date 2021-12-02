using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Receipts_Server.DataBaseContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Property> Properties { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<ServiceCompany> ServiceCompanies { get; set; }

        public DbSet<Tariff> Tariffs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>(entity =>
            {
                entity.Property(o => o.OwnerId)
                    .HasDefaultValueSql("nextval('serial'::regclass)");
            });

            modelBuilder.HasSequence("serial").StartsAt(100000000);
        }
    }
}
