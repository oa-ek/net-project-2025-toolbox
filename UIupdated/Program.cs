using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UIupdated.Components;
using UIupdated.Components.Account;
using UIupdated.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Core;
using Core.Mappings;
using Core.DTOs;
using Repository;
using UIinterface.Services; // [ДОДАНО]

namespace UIupdated
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents(); // [ВИПРАВЛЕНО: перенесено сюди]


            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddScoped<IdentityRedirectManager>();

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = IdentityConstants.ApplicationScheme;
                    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddIdentityCookies();
            builder.Services.AddAuthorization();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDbContext<TTContext>(options => options.UseSqlServer(connectionString));          // [ДОДАНО]

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

          
            // GOOGLE AUTH

            builder.Services.AddAuthentication()
                 .AddGoogle(options =>
                 {
                     options.ClientId = builder.Configuration["Google:ClientId"];
                     options.ClientSecret = builder.Configuration["Google:ClientSecret"];
                     options.CallbackPath = "/signin-google"; // 
                 });


            builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();



            //            // налаштування Identity + EmailSender
            //            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            //            {
            //                options.SignIn.RequireConfirmedEmail = true; // ?? важливо
            //            })
            //.AddEntityFrameworkStores<ApplicationDbContext>();

            //            builder.Services.AddTransient<IEmailSender, SmtpEmailSender>(); // ?? Реалізуємо далі
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // [ДОДАНО]

            builder.Services.AddAutoMapper(typeof(MappingProfile)); // [ДОДАНО]

            // [ДОДАНО] DTO-Сервіси
            builder.Services.AddScoped<IBaseService<BrandDto>, BaseService<Brand, BrandDto>>();
            builder.Services.AddScoped<IBaseService<BataryDto>, BaseService<Batary, BataryDto>>();
            builder.Services.AddScoped<IBaseService<BataryModelDto>, BaseService<BataryModel, BataryModelDto>>();
            builder.Services.AddScoped<IBaseService<BossDto>, BaseService<Boss, BossDto>>();
            builder.Services.AddScoped<IBaseService<ConditionDto>, BaseService<Condition, ConditionDto>>();
            builder.Services.AddScoped<IBaseService<HandToolDto>, BaseService<HandTool, HandToolDto>>();
            builder.Services.AddScoped<IBaseService<LocationDto>, BaseService<Location, LocationDto>>();
            builder.Services.AddScoped<IBaseService<PositionDto>, BaseService<Position, PositionDto>>();
            builder.Services.AddScoped<IBaseService<PowerSupplyTypeDto>, BaseService<PowerSupplyType, PowerSupplyTypeDto>>();
            builder.Services.AddScoped<IBaseService<PowerToolDto>, BaseService<PowerTool, PowerToolDto>>();
            builder.Services.AddScoped<IBaseService<SystemAdminDto>, BaseService<SystemAdmin, SystemAdminDto>>();
            builder.Services.AddScoped<IBaseService<ToolModelDto>, BaseService<ToolModel, ToolModelDto>>();
            builder.Services.AddScoped<IBaseService<ToolTypeDto>, BaseService<ToolType, ToolTypeDto>>();
            builder.Services.AddScoped<IBaseService<WorkerDto>, BaseService<Worker, WorkerDto>>();

            // [ДОДАНО] Користувацькі сервіси
            builder.Services.AddScoped<LocationService>();
            builder.Services.AddScoped<BossService>();
            builder.Services.AddScoped<SystemAdminService>();
            builder.Services.AddScoped<WorkerService>();
            builder.Services.AddScoped<PositionService>();
            builder.Services.AddScoped<HandToolService>();
            builder.Services.AddScoped<PowerToolService>();
            builder.Services.AddScoped<PowerSupplyTypeService>();
            builder.Services.AddScoped<BrandService>();
            builder.Services.AddScoped<BataryService>();
            builder.Services.AddScoped<BataryModelService>();
            builder.Services.AddScoped<ConditionService>();
            builder.Services.AddScoped<ToolModelService>();
            builder.Services.AddScoped<ToolTypeService>();

            // [ДОДАНО] Репозиторії
            builder.Services.AddScoped<BaseRepository<Brand>>();
            builder.Services.AddScoped<BaseRepository<Batary>>();
            builder.Services.AddScoped<BaseRepository<BataryModel>>();
            builder.Services.AddScoped<BaseRepository<Boss>>();
            builder.Services.AddScoped<BaseRepository<Condition>>();
            builder.Services.AddScoped<BaseRepository<HandTool>>();
            builder.Services.AddScoped<BaseRepository<Location>>();
            builder.Services.AddScoped<BaseRepository<Position>>();
            builder.Services.AddScoped<BaseRepository<PowerSupplyType>>();
            builder.Services.AddScoped<BaseRepository<PowerTool>>();
            builder.Services.AddScoped<BaseRepository<SystemAdmin>>();
            builder.Services.AddScoped<BaseRepository<ToolModel>>();
            builder.Services.AddScoped<BaseRepository<ToolType>>();
            builder.Services.AddScoped<BaseRepository<Worker>>();
            builder.Services.AddScoped<RepositoryContainer>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

           /* app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");*/


            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>();

            // Add additional endpoints required by the Identity /Account Razor components.
            app.MapAdditionalIdentityEndpoints();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
