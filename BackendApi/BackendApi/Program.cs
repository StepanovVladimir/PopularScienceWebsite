using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApi.Data;
using BackendApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BackendApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var scope = host.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var passwordHasher = scope.ServiceProvider.GetRequiredService<PasswordHasher<User>>();

            var adminRole = new Role { Name = "Admin" };
            var moderatorRole = new Role { Name = "Moderator" };
                
            if (!context.Roles.Any())
            {
                context.Add(adminRole);
                context.Add(moderatorRole);
                context.SaveChanges();
            }

            if (!context.Users.Any(u => u.Name == "admin"))
            {
                var adminUser = new User { Name = "admin" };
                adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "qwerty");
                context.Add(adminUser);
                context.SaveChanges();

                context.Add(new UserRole { UserId = adminUser.Id, RoleId = adminRole.Id });
                context.Add(new UserRole { UserId = adminUser.Id, RoleId = moderatorRole.Id });
                context.SaveChanges();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
