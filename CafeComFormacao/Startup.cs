using CafeComFormacao.Data;
using CafeComFormacao.Services;
using CafeComFormacao.Repositories;
using CafeComFormacao.Interfaces;

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

            services.AddAuthentication("CookieAuthentication").AddCookie("CookieAuthentication", option =>
            {
                option.LoginPath = "/Login/Index";
                option.AccessDeniedPath = "/Login/Ops";
            });

            services.AddControllersWithViews();

            services.AddScoped<IEventoRepository, EventoRepository>();
            services.AddScoped<IParticipanteRepository, ParticipanteRepository>();
            services.AddScoped<IViewsModelsService, ViewsModelsService>();
            services.AddScoped<IViewsModelsRepository, ViewsModelsRepository>();
            services.AddScoped<ILoginService, LoginService>();

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
