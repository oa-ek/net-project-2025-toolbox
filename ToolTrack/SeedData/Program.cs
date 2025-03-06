using SeedData;
using ToolTrack.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Core;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Налаштування Service Collection
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        // Побудова Service Provider
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Отримання TTContext з Service Provider
        using (var scope = serviceProvider.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<TTContext>();

                // Переконайтесь, що база даних створена
                context.Database.Migrate();

                // Виклик Seed.Initialize для заповнення даних
                var repositories = new RepositoryContainer(context);
                await Seed.InitializeAsync(repositories);

                Console.WriteLine("База даних успішно заповнена тестовими даними!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка під час заповнення бази даних: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
            }
        }
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Налаштування контексту бази даних
        services.AddDbContext<TTContext>(options =>
            options.UseSqlServer("Server=localhost,1433;Database=ToolTrackDB;User Id=sa;Password=ToolTrack123!;TrustServerCertificate=True;"));

        // Додайте тут інші необхідні сервіси
    }
}
