using DynamicAuthorization.Mvc.Core.Extensions;
using DynamicAuthorization.Mvc.MsSqlServerStore;
using DynamicAuthorization.Mvc.Ui;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stilosoft.Business.Abstract;
using Stilosoft.Business.Business;
using Stilosoft.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stilosoft
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(5);//You can set Time   
            });

            var conexion = Configuration["ConnectionStrings:conexion_sqlServer"];
            services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(conexion));

            services.AddScoped<IServicioService, ServicioService>();
            services.AddScoped<IEstilistaService, EstilistaService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IComprasService, ComprasService>();
            services.AddScoped<IProveedorService, ProveedorService>();
            services.AddScoped<IAbonoCompraService, AbonoCompraService>();
            services.AddScoped<IDetalleCompraService, DetalleCompraService>();
            services.AddScoped<ICitaService, CitaService>();
            services.AddScoped<IProveedorService, ProveedorService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ISolicitudService, SolicitudService>();


            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
            });

            //services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();
            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<AppDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/Usuarios/Login");
                options.Cookie.Name = "Cookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
                options.LoginPath = new PathString("/Usuarios/Login");
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Landing}/{action=Index}/{id?}");
            });
        }
    }
}
