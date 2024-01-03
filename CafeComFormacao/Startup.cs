﻿using CafeComFormacao.Data;
using Microsoft.EntityFrameworkCore;

namespace CafeComFormacao
{
    public class Startup
    {
        public Startup(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CafeComFormacaoContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("CafeComFormacaoContext"), new MySqlServerVersion(new Version(8, 0, 35)), builder =>
                builder.MigrationsAssembly("CafeComFormacao")));

            services.AddControllersWithViews();
        }

        public void Configure(WebApplication app, IWebHostEnvironment enviroment)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}