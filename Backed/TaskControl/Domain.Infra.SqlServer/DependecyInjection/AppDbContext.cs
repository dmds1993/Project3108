using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Domain.Infra.SqlServer.DependecyInjection
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<PBIEntity> PBIs { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskEntity>()
                .HasOne(t => t.PBI)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.PBIId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}