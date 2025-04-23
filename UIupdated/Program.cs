using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UIupdated.Components;
using UIupdated.Components.Account;
using UIupdated.Data;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace UIupdated
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents();

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
                     options.CallbackPath = "/signin-google"; // ??????? ??? ?????, ???? ??? ?????????
                 });


//            // налаштування Identity + EmailSender
//            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
//            {
//                options.SignIn.RequireConfirmedEmail = true; // ?? важливо
//            })
//.AddEntityFrameworkStores<ApplicationDbContext>();

//            builder.Services.AddTransient<IEmailSender, SmtpEmailSender>(); // ?? Реалізуємо далі



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

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>();

            // Add additional endpoints required by the Identity /Account Razor components.
            app.MapAdditionalIdentityEndpoints();

            app.Run();
        }
    }
}
