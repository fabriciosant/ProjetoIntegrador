using LoKMais.Data;
using LoKMais.Models;
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
            services.AddDbContext<Contexto>(options => options.UseSqlServer(Configuration.GetConnectionString("DBLokMais"), p => p.MigrationsHistoryTable("HistoricoDasMigrastions", "LokMais"))).AddIdentity<Usuario, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(5);
            })
                .AddEntityFrameworkStores<Contexto>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<PortugueseIdentityErrorDescriber>();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Autenticador/Login");
                options.LogoutPath = new PathString("Autenticador/Logout");
                options.AccessDeniedPath = new PathString("Autenticador/AcessoNegado");
                options.ExpireTimeSpan = TimeSpan.FromDays(5);
                options.SlidingExpiration = true;
            });
            services.AddControllersWithViews()
                .AddNToastNotifyToastr(new ToastrOptions()
                {
                    ProgressBar = true,
                    PositionClass = ToastPositions.TopRight
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
                    pattern: "{controller=Autenticador}/{action=Login}/{id?}");
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
            using var contexto = serviceProvider.GetRequiredService<Contexto>();
            using var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();
            var userexists = await userManager.FindByNameAsync("fabriciosa47@gmail.com");

            if (userexists == null)
            {
                var cpf = new CPF("064.608.675-85");
                cpf.SemFormatacao();

                var usuario = new Usuario  
                {
                    Email = "Fabriciosan47@gmail.com"
                };

                var resultRegisterAccount = await userManager.CreateAsync(usuario, "F@bricio20");
                if (resultRegisterAccount.Succeeded)
                {
                    await userManager.AddToRoleAsync(usuario, "ADMINISTRADOR");
                }
            }
        }
    }
}
