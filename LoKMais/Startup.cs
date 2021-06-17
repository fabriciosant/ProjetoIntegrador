using LoKMais.Data;
using LoKMais.Interfaces;
using LoKMais.Models;
using LoKMais.Models.ViewModels;
using LoKMais.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LkContextDB>(options => options.UseSqlServer(Configuration.GetConnectionString("DBLokMais"), p => p.MigrationsHistoryTable("HistoricoDasMigrastions", "LokMais"))).AddIdentity<Cliente, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(5);
            })
                .AddEntityFrameworkStores<LkContextDB>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<PortugueseIdentityErrorDescriber>();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Autenticador/Login");
                options.LogoutPath = new PathString("/Autenticador/Logout");
                options.AccessDeniedPath = new PathString("/Autenticador/AcessoNegado");
                options.ExpireTimeSpan = TimeSpan.FromDays(5);
                options.SlidingExpiration = true;
            });
            services.AddControllersWithViews()
                .AddNToastNotifyToastr(new ToastrOptions()
                {
                    ProgressBar = true,
                    PositionClass = ToastPositions.TopRight
                });
            services.AddScoped<IVeiculoRepository, VeiculoRepository>();
        }

        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env, 
            IServiceProvider pro)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error").UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseNToastNotify();

            CreateRoleClaimsAsync(pro).Wait();
            CreateAdministradorAsync(pro).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public async Task CreateRoleClaimsAsync(IServiceProvider serviceProvider)
        {
            using var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var roles = await roleManager.Roles.ToListAsync();

            if (!roles.Any())
            {
                string[] rolesNames = { "ADMINISTRADOR", "LokMais" };
                foreach (var namesRole in rolesNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(namesRole);
                    if (!roleExist)
                    {
                        var role = new IdentityRole<Guid>(namesRole);
                        await roleManager.CreateAsync(role);
                    }
                }
            }
        }

        public async Task CreateAdministradorAsync(IServiceProvider serviceProvider)
        {
            using var contexto = serviceProvider.GetRequiredService<LkContextDB>();
            using var userManager = serviceProvider.GetRequiredService<UserManager<Cliente>>();
            var userexists = await userManager.FindByNameAsync("fabriciosa47@gmail.com");

            if (userexists == null)
            {
                var cpf = new CPF("886.922.770-70");
                cpf.SemFormatacao();

                var usuario = new Cliente()  
                {
                    UserName = cpf.Codigo,
                    Email = "Fabriciosan47@gmail.com"
                };

                var resultRegisterAccount = await userManager.CreateAsync(usuario, "Senh@0102");
                if (resultRegisterAccount.Succeeded)
                {
                    await userManager.AddToRoleAsync(usuario, "ADMINISTRADOR");
                }
            }
        }
    }
}
