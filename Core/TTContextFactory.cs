/*using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

public class TTContextFactory : IDesignTimeDbContextFactory<TTContext>
{
    public TTContext CreateDbContext(string[] args)
    {
        // Знайдіть ваш appsettings.json (шлях може відрізнятися)
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
*/