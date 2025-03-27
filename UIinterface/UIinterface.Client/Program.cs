using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace UIinterface.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddAuthorizationCore();
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddAuthenticationStateDeserialization();

            // Реєстрація HttpClient та сервісів для роботи з HTTP-клієнтом
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<BataryService>();
            builder.Services.AddScoped<BrandService>();
            builder.Services.AddScoped<BataryModelService>();
            builder.Services.AddScoped<BossService>();
            builder.Services.AddScoped<ConditionService>();
            builder.Services.AddScoped<HandToolService>();
            builder.Services.AddScoped<LocationService>();
            builder.Services.AddScoped<PositionService>();
            builder.Services.AddScoped<PowerSupplyTypeService>();
            builder.Services.AddScoped<PowerToolService>();
            builder.Services.AddScoped<SystemAdminService>();
            builder.Services.AddScoped<ToolModelService>();
            builder.Services.AddScoped<ToolTypeService>();
            builder.Services.AddScoped<WorkerService>();

            await builder.Build().RunAsync();
        }
    }
}
