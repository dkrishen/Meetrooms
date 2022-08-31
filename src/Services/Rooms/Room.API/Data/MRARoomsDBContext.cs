using MRA.Rooms.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace MRA.Rooms.Data
{
    public partial class MRARoomsDBContext : DbContext
    {
        public MRARoomsDBContext(DbContextOptions<MRARoomsDBContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public virtual DbSet<Room> Rooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    Configuration["Data:Database:ConnectionString"],
                    new MySqlServerVersion(new Version(8, 0, 30))
                    );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(UUID())");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });
            modelBuilder.Entity<Room>().HasData(
                new Room[]
                {
                    new Room { Id = new Guid("1DDA7260-08E8-4B32-A9EE-F7E1CA69BC9C"), Name = "213 Room", Description = null }
                });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
