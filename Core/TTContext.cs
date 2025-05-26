using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace Core
{
    public class TTContext : IdentityDbContext<AppUser>
    {
        public TTContext(DbContextOptions<TTContext> options) : base(options)
        { }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ToolType> ToolTypes { get; set; }
        public DbSet<PowerSupplyType> PowerSupplyTypes { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<ToolModel> ToolModels { get; set; }
        public DbSet<BataryModel> BataryModels { get; set; }
        public DbSet<Batary> Bataries { get; set; }
        public DbSet<PowerTool> PowerTools { get; set; }
        public DbSet<HandTool> HandTools { get; set; }
        public DbSet<Boss> Bosses { get; set; }
        public DbSet<SystemAdmin> SystemAdmins { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Worker> Workers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HandTool>()
                .HasOne(ht => ht.LastWorker)
                .WithMany()
                .HasForeignKey(ht => ht.LastWorkerId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<PowerTool>()
                .HasOne(pt => pt.LastWorker)
                .WithMany()
                .HasForeignKey(pt => pt.LastWorkerId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Batary>()
                .HasOne(b => b.LastWorker)
                .WithMany()
                .HasForeignKey(b => b.LastWorkerId)
                .OnDelete(DeleteBehavior.SetNull);
        }

    }
}
