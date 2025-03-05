using Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToolTrack.ManageUI.Data;
using ToolTrack.Repository;
using ToolTrack.Repository.Repos;

var builder = WebApplication.CreateBuilder(args);

// Отримуємо рядок підключення
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Додаємо контекст бази даних TTContext
builder.Services.AddDbContext<TTContext>(options =>
    options.UseSqlServer(connectionString));

// Додаємо контекст для Identity (якщо використовуєш окремий)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Додаємо підтримку помилок розробника
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Реєстрація базового репозиторію та його реалізацій
builder.Services.AddScoped(typeof(BaseInterface<>), typeof(BaseRepository<>));

// Реєстрація репозиторіїв
builder.Services.AddScoped<BataryRepository>();
builder.Services.AddScoped<BataryModelRepository>();
builder.Services.AddScoped<BrandRepository>();
builder.Services.AddScoped<ConditionRepository>();
builder.Services.AddScoped<HandToolRepository>();
builder.Services.AddScoped<PowerSupplyTypeRepository>();
builder.Services.AddScoped<PowerToolRepository>();
builder.Services.AddScoped<ToolModelRepository>();
builder.Services.AddScoped<ToolTypeRepository>();
builder.Services.AddScoped<BossRepository>();
builder.Services.AddScoped<LocationRepository>();
builder.Services.AddScoped<PositionRepository>();
builder.Services.AddScoped<SystemAdminRepository>();
builder.Services.AddScoped<WorkerRepository>();
builder.Services.AddScoped<WorkStatisticRepository>();

// Налаштування Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages().WithStaticAssets();

app.Run();
