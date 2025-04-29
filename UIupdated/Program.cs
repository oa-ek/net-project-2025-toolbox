using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Components.Authorization;
using UIupdated.Components;
using UIupdated.Components.Account;
using UIupdated.Data;
using Core;
using Core.DTOs;
using Core.Mappings;
using Repository;
using UIinterface.Services; // Або UIupdated.Services, якщо перенесеш туди
using System;
using UIinterface.Components.Account;

namespace UIupdated
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // --- Сервіси та базові сервіси ---
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

            // --- Конкретні сервіси ---
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

            // --- Репозиторії ---
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

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Razor Components
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddScoped<IdentityRedirectManager>();

            // Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            }).AddIdentityCookies();

            builder.Services.AddAuthorization();

            // DbContexts
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<TTContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Google Auth
            builder.Services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = builder.Configuration["Google:ClientId"];
                options.ClientSecret = builder.Configuration["Google:ClientSecret"];
                options.CallbackPath = "/signin-google";
            });

            // App pipeline
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAntiforgery();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
            app.MapAdditionalIdentityEndpoints();

            app.Run();
        }
    }
}
