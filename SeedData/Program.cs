using SeedData;
using Repository;
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
       
            try
            {
            var context = new TTContext();

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