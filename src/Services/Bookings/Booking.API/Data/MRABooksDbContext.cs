using System;
using MRA.Bookings.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MRA.Bookings.Data
{
    public partial class MRABooksDbContext : DbContext
    {
        public IConfiguration Configuration { get; }

        public MRABooksDbContext(DbContextOptions<MRABooksDbContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public virtual DbSet<Book> Books { get; set; }

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
            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id")
                    .HasDefaultValueSql("(UUID())");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.EndTime).HasColumnName("endTime");

                entity.Property(e => e.MeetingRoomId).HasColumnName("meetingRoomId");

                entity.Property(e => e.StartTime).HasColumnName("startTime");

                entity.Property(e => e.UserId).HasColumnName("userId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}