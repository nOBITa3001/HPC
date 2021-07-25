using HPC.Domain.Entities;
using HPC.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPC.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var administrator = new ApplicationUser { UserName = "admin@hpc.io", Email = "admin@hpc.io" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "P@ssw0rd");
                await userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            var random = new Random();
            var ships = new List<(string Name, string Code)>
            {
                ("Allan Ship", "ALAL-1111-A1"),
                ("Leo Ship", "LELE-1111-L1"),
                ("Niko Ship", "NINI-1111-N1"),
                ("North Ship", "NONO-1111-N1"),
            };

            if (context.Ships.Any() is false)
            {
                ships.ForEach(ship => context.Ships.Add
                (
                    new Ship
                    {
                        Name = ship.Name,
                        Code = ship.Code,
                        LengthInMetres = (decimal)random.NextDouble() * 1000,
                        WidthInMetres = (decimal)random.NextDouble() * 100
                    }
                ));
                
                await context.SaveChangesAsync();
            }
        }
    }
}
