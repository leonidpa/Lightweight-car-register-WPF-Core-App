using CarsRepositoryLibrary.Models;
using CarsRepositoryLibrary.Repositories;
using CarsRepositoryLibrary.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarsRepositoryLibrary.Contexts
{
    class CarsDbContext : DbContext
    {
        private string ConnectionString = RepositoryService.ConnectionString<CarsRepository>();

        public DbSet<Car> Cars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(ConnectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Model).IsRequired();
                entity.Property(e => e.Brand).IsRequired();
            });
        }
    }
}
