using FCG.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FCG.Infra.Data.Seedings
{
    public static class RoleAndAdminSeeding
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            try
            {
                Console.WriteLine("Starting role and admin user seed...");

                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

                var roles = new[] { "User", "Admin" };

                foreach (var roleName in roles)
                {
                    var exists = await roleManager.RoleExistsAsync(roleName);
                    if (!exists)
                    {
                        var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                        Console.WriteLine(result.Succeeded
                            ? $"Role '{roleName}' created."
                            : $"Failed to create role '{roleName}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                    else
                    {
                        Console.WriteLine($"Role '{roleName}' already exists.");
                    }
                }

                var adminEmail = "admin@fiap.com.br";
                var adminPassword = "Admin1234!";

                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    adminUser = new User("Admin System", adminEmail)
                    {
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(adminUser, adminPassword);

                    if (result.Succeeded)
                    {
                        Console.WriteLine("Admin user created.");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
                else
                {
                    Console.WriteLine("Admin user already exists.");
                }

                var inRole = await userManager.IsInRoleAsync(adminUser, "Admin");
                if (!inRole)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    Console.WriteLine("Admin user added to Admin role.");
                }

                Console.WriteLine("Role and admin user seed completed!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on role/admin seed: {ex.Message}");
            }
        }
    }
}
