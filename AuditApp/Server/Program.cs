using AuditApp.Server.Controllers;
using AuditApp.Server.Database;
using AuditApp.Server.Database.Repositories;
using AuditApp.Server.Objects;
using AuditApp.Server.Services;
using AuditApp.Shared.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AuditApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connString = "Server=localhost;User Id=root;Password=;Database=audit;DefaultCommandTimeout=60; Allow User Variables=true";
            //todo ver como obter isso do appsettings.json
            builder.Services.AddDbContext<AuditContext>(options => options.UseMySql(connString, ServerVersion.AutoDetect(connString)));
            builder.Services.AddScoped<DbContext, AuditContext>();
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
            builder.Services.AddScoped<JwtTokenHandler>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<EmailService>();
            builder.Services.AddScoped<AnswerService>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();


            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}