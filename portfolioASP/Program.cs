using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using portfolio.DataAccess.Data;
using portfolio.DataAccess.Json;
using portfolio.DataAccess.Repository;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;
using portfolio.Models.Email;
using portfolio.Utility;
using portfolio.Utility.Email;
using System.Configuration;
using System.Net;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace portfolioASP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            builder.Services.AddLocalization(options =>
            {
                options.ResourcesPath = "Languages";
            });

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-GB"),
                    new CultureInfo("pl")
                };

                options.DefaultRequestCulture = new RequestCulture("en-GB");
                options.SupportedUICultures = supportedCultures;

            });

            builder.Services.AddDbContext<ApplicationDbContext>
                (options=> options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
            builder.Services.Configure<AdminLogin>(builder.Configuration.GetSection("AdminLogin"));
            builder.Services.AddTransient<IEmailService, EmailService>();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            var app = builder.Build();

            app.UseRequestLocalization();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=View}/{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
