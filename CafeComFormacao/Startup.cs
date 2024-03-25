using CafeComFormacao.Data;
using CafeComFormacao.Services;
using CafeComFormacao.Models;
using CafeComFormacao.Repositories;
using CafeComFormacao.Interfaces.Repositories;
using CafeComFormacao.Interfaces.Services;
using CafeComFormacao.Interfaces.Util;

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
                                                                    options.UseMySql(Configuration.GetConnectionString("CafeComFormacaoContext"),
                                                                    new MySqlServerVersion(new Version(8, 0, 35)),
                                                                    builder =>
                                                                              builder.MigrationsAssembly("CafeComFormacao")));

            services.AddAuthentication("CookieAuthentication").AddCookie("CookieAuthentication", option =>
            {
                option.LoginPath = "/Login/Index";
                option.AccessDeniedPath = "/Login/Ops";
                option.ExpireTimeSpan = TimeSpan.FromHours(1);
            });

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "TokenCSRFDeValidação";
            });

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            services.AddControllersWithViews();

            services.AddScoped<IHashService, HashService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ISanitizarService, SanitizarService>();
            services.AddScoped<IEventoRepository, EventoRepository>();
            services.AddScoped<IParticipanteRepository, ParticipanteRepository>();
            services.AddScoped<IViewsModelsService, ViewsModelsService>();
            services.AddScoped<IEventoService, EventoService>();
            services.AddScoped<IViewsModelsRepository, ViewsModelsRepository>();
            services.AddScoped<IParticipanteService, ParticipanteService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IPalestranteService, PalestranteService>();
            services.AddScoped<IPalestranteRepository, PalestranteRepository>();

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
