namespace Simple.Identity.Api.Domain.Entities.Context
{
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public class DummyData
    {
        public static async Task Initialize(AppIdentityDbContext context,
                              UserManager<ApplicationUser> userManager,
                              RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();


            string role1 = "Admin";

            string role2 = "Member";

            string password = "P@$$w0rd";

            if (await roleManager.FindByNameAsync(role1) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role1));
            }
            if (await roleManager.FindByNameAsync(role2) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role2));
            }

            if (await userManager.FindByNameAsync("admin1@phungdkh.com.vn") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "admin1@phungdkh.com.vn",
                    Email = "admin1@phungdkh.com.vn",
                    PhoneNumber = "0919121212"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role1);
                }
            }

            if (await userManager.FindByNameAsync("admin2@phungdkh.com.vn") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "admin2@phungdkh.com.vn",
                    Email = "admin2@phungdkh.com.vn",
                    PhoneNumber = "0918888888"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role1);
                }
            }
        }
    }
}
