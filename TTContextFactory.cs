using Microsoft.EntityFrameworkCore;

namespace Core
{
    public class TTContext : DbContext
    {
        private TTContext(DbContextOptions<TTContext> options) : base(options) { } //зробив конструктор приватним
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ToolType> ToolTypes { get; set; }
        public DbSet<PowerSupplyType> PowerSupplyTypes { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<ToolModel> ToolModels { get; set; }
        public DbSet<BataryModel> BataryModels { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Boss> Bosses { get; set; }
        public DbSet<SystemAdmin> SystemAdmins { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<WorkStatistic> WorkStatistics { get; set; }
        public DbSet<PowerTool> PowerTools { get; set; }
        public DbSet<HandTool> HandTools { get; set; }
        public DbSet<Batary> Bataries { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=localhost,1433;Database=ToolTrackDB1;User Id=sa;Password=ToolTrack123!;TrustServerCertificate=True;");
        }


    }

}
