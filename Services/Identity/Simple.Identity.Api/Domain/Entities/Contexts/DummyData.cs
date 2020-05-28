namespace Simple.Identity.Api.Domain.Entities.Context
{
    using Microsoft.AspNetCore.Identity;
    using Simple.Core.Models.Common;
    using System.Threading.Tasks;

    public class DummyData
    {
        public static async Task Initialize(AppIdentityDbContext context,
                              UserManager<ApplicationUser> userManager,
                              RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            string password = "P@$$w0rd";

            if (await roleManager.FindByNameAsync(CommonConstants.Roles.ADMIN) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(CommonConstants.Roles.ADMIN));
            }
            if (await roleManager.FindByNameAsync(CommonConstants.Roles.MEMBER) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(CommonConstants.Roles.MEMBER));
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
                    await userManager.AddToRoleAsync(user, CommonConstants.Roles.ADMIN);
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
                    await userManager.AddToRoleAsync(user, CommonConstants.Roles.ADMIN);
                }
            }
        }
    }
}
