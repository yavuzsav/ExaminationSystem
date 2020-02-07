using ExaminationSystem.DataAccess.Concrete.EntityFramework.Contexts;
using ExaminationSystem.Framework.DependencyResolvers;
using ExaminationSystem.Framework.Extensions;
using ExaminationSystem.Framework.Utilities.IoC;
using ExaminationSystem.Models.IdentityEntities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ExaminationSystem.MvcCoreWebUI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDependencyResolvers(new ICoreModule[]
            {
                new CoreModule()
            });

            services.AddDbContext<ExaminationSystemContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(Configuration.GetConnectionString("ExaminationSystem"));
            });

            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                //options.UseSqlServer(Configuration["ConnectionString:DefaultConnectionString"]);
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
            });

            services.AddIdentity<AppUser, AppRole>(options =>
                {
                    options.Password.RequiredLength = 4; //Todo : 8 yapýlabilir.
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;

                    options.User.RequireUniqueEmail = true;
                    options.User.AllowedUserNameCharacters = "abcçdefgðhýijklmnopqrsþtuüvwxyzABCÇDEFGÐHIÝJKLMNOPQRSÞTUÜVWXYZ0123456789-._";
                }).AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();

            CookieBuilder cookieBuilder = new CookieBuilder
            {
                Name = "ExaminationSystem",
                HttpOnly = false,
                SameSite = SameSiteMode.Lax, //güvenlik için Strict
                SecurePolicy = CookieSecurePolicy.SameAsRequest
            };

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Home/Login");
                options.LogoutPath = new PathString("/User/SignOut");
                options.Cookie = cookieBuilder;
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(60);
                options.AccessDeniedPath = new PathString("/Member/AccessDenied");
            });

            services.AddMvc(option => { option.EnableEndpointRouting = false; })
                .AddFluentValidation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseHsts();
            app.ConfigureCustomExceptionMiddleware();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMvcWithDefaultRoute();
        }
    }
}