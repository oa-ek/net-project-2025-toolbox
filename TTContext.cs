using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core
{
    public class TTContext : IdentityDbContext<AppUser>
    {
        public TTContext(DbContextOptions<TTContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

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
    }
    public class TTContextFactory : IDesignTimeDbContextFactory<TTContext>
    {
        public TTContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<TTContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);

            return new TTContext(optionsBuilder.Options);
        }
    }
}
