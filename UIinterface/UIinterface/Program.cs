using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UIinterface.Components;
using UIinterface.Components.Account;
using UIinterface.Data;
using Core;
using Core.Mappings;
using Core.DTOs;
using Repository;
using UIinterface.Services;

namespace UIinterface
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Додайте реєстрацію сервісів
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


            builder.Services.AddScoped<LocationService>();

            // Реєстрація BaseRepository для кожної сутності
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
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddAuthenticationStateSerialization();

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
                .AddIdentityCookies();

            // Get connection string once
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            // Register main DbContext for app data
            builder.Services.AddDbContext<TTContext>(options =>
                options.UseSqlServer(connectionString));

            // Register ApplicationDbContext for Identity
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Configure Identity with ApplicationDbContext
            builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            // Add Identity endpoints.
            app.MapAdditionalIdentityEndpoints();

            app.Run();
        }
    }
}





