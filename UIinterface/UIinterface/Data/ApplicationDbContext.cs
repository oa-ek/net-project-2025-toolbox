using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Core; // Додайте цей using, якщо ваші сутності знаходяться у просторі Core

namespace UIinterface.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Додаємо потрібні DbSet-и для інструментів
        public DbSet<PowerTool> PowerTools { get; set; }
        public DbSet<HandTool> HandTools { get; set; }
        public DbSet<Batary> Bataries { get; set; }
    }
}
