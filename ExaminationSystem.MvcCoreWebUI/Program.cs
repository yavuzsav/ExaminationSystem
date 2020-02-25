using Autofac;
using Autofac.Extensions.DependencyInjection;
using ExaminationSystem.Business.DependencyResolvers.Autofac;
using ExaminationSystem.DataAccess.Concrete.EntityFramework.Contexts;
using ExaminationSystem.DataAccess.Concrete.EntityFramework.SeedData;
using ExaminationSystem.Models.IdentityEntities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace ExaminationSystem.MvcCoreWebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ExaminationSystemContext>();
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
                    context.Database.Migrate();
                    SeedData.Seed(context, userManager, roleManager).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured during migration");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder =>
                    {
                        builder.RegisterModule(new AutofacBusinessModule());
                        builder.RegisterModule(new AutofacValidationModule());
                    })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}